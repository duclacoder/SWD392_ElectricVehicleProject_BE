using AutoMapper;
using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using EV.Domain.Entities;

namespace EV.Application.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<CarAddResponseDTO>> AddCar(CarAddRequestDTO carAddRequestDTO)
        {
            if (carAddRequestDTO.Year.HasValue && carAddRequestDTO.Year > DateTime.Now.Year)
            {
                return new ResponseDTO<CarAddResponseDTO>(
                    $"Invalid Year: {carAddRequestDTO.Year}. Year cannot be in the future.",
                    false,
                    null
                );
            }

            if (carAddRequestDTO.Year.HasValue && carAddRequestDTO.Km.HasValue)
            {
                var carAge = DateTime.Now.Year - carAddRequestDTO.Year.Value;
                var maxExpectedKm = (carAge < 0 ? 0 : carAge) * 50000 + 100000; // 50k km/year + buffer

                if (carAddRequestDTO.Km.Value > maxExpectedKm)
                {
                    return new ResponseDTO<CarAddResponseDTO>(
                        $"Unrealistic Km value ({carAddRequestDTO.Km.Value}) for a {carAddRequestDTO.Year.Value} car. Max expected: {maxExpectedKm}.",
                        false,
                        null
                    );
                }
            }

            var addedVehicle = _mapper.Map<Vehicle>(carAddRequestDTO);

            try
            {
                _unitOfWork.GetGenericRepository<Vehicle>().CreateAsync(addedVehicle);

                await _unitOfWork.SaveChangesAsync();

                var addResult = _mapper.Map<CarAddResponseDTO>(addedVehicle);

                return new ResponseDTO<CarAddResponseDTO>("Car added successfully", true, addResult);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CarAddResponseDTO>($"Error adding car: {ex.Message}", false, null);
            }
        }

        public async Task<ResponseDTO<PagedResult<UserCarGetAll>>> UserCarGetAll(UserGetAllCarRequestDTO userGetAllCarRequestDTO)
        {
            var cars = await _unitOfWork.carRepository.GetAllCarByUserId(userGetAllCarRequestDTO.UserId, (userGetAllCarRequestDTO.Page - 1) * userGetAllCarRequestDTO.PageSize
                , userGetAllCarRequestDTO.PageSize);
            var totalItems = await _unitOfWork.carRepository.GetTotalCountCarByUserId(userGetAllCarRequestDTO.UserId);


            var pagedResult = new PagedResult<UserCarGetAll>
            {
                Items = cars.ToList(),
                TotalItems = totalItems,
                Page = userGetAllCarRequestDTO.Page,
                PageSize = userGetAllCarRequestDTO.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalItems / userGetAllCarRequestDTO.PageSize)
            };
            if (cars.Count() == 0)
            {
                return new ResponseDTO<PagedResult<UserCarGetAll>>("Can not find any car", false, null);
            }


            return new ResponseDTO<PagedResult<UserCarGetAll>>("Get all cars successfully", true, pagedResult);
        }

        public async Task<ResponseDTO<UserCarDetails>> UserCarViewDetailsById(UserViewCarDetailsRequestDTO userViewCarDetailsRequestDTO)
        {
            var carDetails = await _unitOfWork.carRepository.UserCarViewDetailsById(userViewCarDetailsRequestDTO.UserId, userViewCarDetailsRequestDTO.VehicleId);
            
            if (carDetails == null)
            {
                return new ResponseDTO<UserCarDetails>("Can not find the car", false, null);
            }
            return new ResponseDTO<UserCarDetails>("Get car details successfully", true, carDetails);
        }

        //public async Task<ResponseDTO<UserCarDetails>> UserGetCarsDetailsForUpdate(UserGetCarDetailsForUpdateDTO userGetCarDetailsForUpdateDTO)
        //{
        //    var car = await _unitOfWork.carRepository.UserCarViewDetailsById(userGetCarDetailsForUpdateDTO.UserId, userGetCarDetailsForUpdateDTO.VehicleId);

        //    if (car == null)
        //    {
        //        return new ResponseDTO<UserCarDetails>("Can not find the car", false, null);
        //    }


        //    return new ResponseDTO<UserCarDetails>("Get car details successfully", true, car);
        //}

        public async Task<ResponseDTO<string>> UserDeleteCar(int userId, int carId)
        {
            var car = await _unitOfWork.carRepository.GetCarForUpdate(userId, carId);
            if (car == null)
            {
                return new ResponseDTO<string>("Car not found or does not belong to the user", false, "Not Found");
            }

            if(car.Status == "Sold")
            {
                return new ResponseDTO<string>("Cannot delete a sold car", false, "Sold");
            }

            if(car.Status == "Auctioned")
            {
                return new ResponseDTO<string>("Cannot delete an auctioned car", false, "Auctioned");
            }

            if(car.Status == "Posted")
            {
                return new ResponseDTO<string>("Cannot delete a posted car", false, "Posted");
            }

            try
            {
                car.Status = "Deleted";

                await _unitOfWork.SaveChangesAsync();
                return new ResponseDTO<string>("Car deleted successfully", true, null);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<string>($"Error deleting car: {ex.Message}", false, null);
            }
        }

        public async Task<ResponseDTO<string>> UserUnDeleteCar(int userId, int carId)
        {
            var car = await _unitOfWork.carRepository.GetCarForUpdate(userId, carId);
            if (car == null)
            {
                return new ResponseDTO<string>("Car not found or does not belong to the user", false, "Not Found");
            }

            if(car.Status != "Deleted")
            {
                return new ResponseDTO<string>("Only deleted cars can be undeleted", false, $"Car current status: {car.Status}");
            }


            try
            {
                car.Status = "Pending";
                car.Verified = false;

                await _unitOfWork.SaveChangesAsync();
                return new ResponseDTO<string>("Car undeleted successfully", true, null);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<string>($"Error undeleting car: {ex.Message}", false, null);
            }
        }

        public async Task<ResponseDTO<UserCarUpdateReponse>> UserCarUpdate(UserCarUpdateRequest userCarUpdateRequest)
        {
            var findCar = await _unitOfWork.carRepository.GetCarForUpdate(userCarUpdateRequest.UserId, userCarUpdateRequest.VehiclesId);

            if (findCar == null)
            {
                return new ResponseDTO<UserCarUpdateReponse>("Car not found or does not belong to the user", false, null);
            }

            if (findCar.Status == "Sold")
            {
                return new ResponseDTO<UserCarUpdateReponse>("Cannot update a sold car", false, null);
            }
            if (findCar.Status == "Auctioned")
            {
                return new ResponseDTO<UserCarUpdateReponse>("Cannot update an auctioned car", false, null);
            }
            if (findCar.Status == "Posted")
            {
                return new ResponseDTO<UserCarUpdateReponse>("Cannot update a posted car", false, null);
            }

            if (userCarUpdateRequest.Year.HasValue && userCarUpdateRequest.Year > DateTime.Now.Year)
            {
                return new ResponseDTO<UserCarUpdateReponse>(
                    $"Invalid Year: {userCarUpdateRequest.Year}. Year cannot be in the future.",
                    false,
                    null
                );
            }
            if (userCarUpdateRequest.Year.HasValue && userCarUpdateRequest.Km.HasValue)
            {
                var carAge = DateTime.Now.Year - userCarUpdateRequest.Year.Value;
                var maxExpectedKm = (carAge < 0 ? 0 : carAge) * 50000 + 100000; // 50k km/year + buffer
                if (userCarUpdateRequest.Km.Value > maxExpectedKm)
                {
                    return new ResponseDTO<UserCarUpdateReponse>(
                        $"Unrealistic Km value ({userCarUpdateRequest.Km.Value}) for a {userCarUpdateRequest.Year.Value} car. Max expected: {maxExpectedKm}.",
                        false,
                        null
                    );
                }
            }


            try
            {
                _mapper.Map(userCarUpdateRequest, findCar);
                findCar.UpdatedAt = DateTime.Now;
                findCar.Verified = false;
                findCar.Status = "Pending";
                await _unitOfWork.SaveChangesAsync();

                var updatedCar = _mapper.Map<UserCarUpdateReponse>(findCar);

                return new ResponseDTO<UserCarUpdateReponse>("Car updated successfully", true, updatedCar);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<UserCarUpdateReponse>($"Error updating car: {ex.Message}", false, null);
            }


        }

        public async Task<ResponseDTO<AuctionVehicleDetails?>> GetCarById(int carId)
        {
            try 
            {
                var car = await _unitOfWork.carRepository.GetAuctionVehicleDetailsById(carId);

                if (car == null)
                {
                    return new ResponseDTO<AuctionVehicleDetails?>("Car not found", false, null);
                }

                return new ResponseDTO<AuctionVehicleDetails?>("Get car successfully", true, car);
            }
            catch(Exception ex)
            {
                return new ResponseDTO<AuctionVehicleDetails?>($"Error retrieving car: {ex.Message}", false, null);
            }
        }
    }
}
