using AutoMapper;
using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using EV.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace EV.Application.Services
{
    public class BatteryService : IBatteryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICloudinaryRepository _cloudinaryRepository;
        public BatteryService(IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryRepository cloudinaryRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinaryRepository = cloudinaryRepository;
        }

        public async Task<ResponseDTO<BatteryAddResponseDTO>> AddBattery(BatteryAddRequestDTO batteryAddRequestDTO, IFormFile imageUpload)
        {
            var addedBattery = _mapper.Map<Battery>(batteryAddRequestDTO);

            try
            {
                await _unitOfWork.GetGenericRepository<Battery>().CreateAsync(addedBattery);

                var addResult = _mapper.Map<BatteryAddResponseDTO>(addedBattery);

                var listImages = new List<BatteryImage>();

                if (addResult != null)
                {
                    var imageUrl = await _cloudinaryRepository.UploadImageToCloudinaryAsync(imageUpload);
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        var batteryImage = new BatteryImage
                        {
                            BatteryId = addedBattery.BatteriesId,
                            ImageUrl = imageUrl,
                        };
                        listImages.Add(batteryImage);
                    }
                }

                addedBattery.BatteryImages = listImages;

                await _unitOfWork.SaveChangesAsync();


                return new ResponseDTO<BatteryAddResponseDTO>("Battery added successfully", true, addResult);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<BatteryAddResponseDTO>($"Error adding battery: {ex.Message}", false, null);
            }
        }

        public async Task<ResponseDTO<PagedResult<UserBatteryGetAll>>> UserBatteryGetAll(UserGetAllBatteryRequestDTO userGetAllBatteryRequestDTO)
        {
            var batteries = await _unitOfWork.batteryRepository.GetAllBatteryByUserId(userGetAllBatteryRequestDTO.UserId, (userGetAllBatteryRequestDTO.Page - 1) * userGetAllBatteryRequestDTO.PageSize
                , userGetAllBatteryRequestDTO.PageSize);
            var totalItems = await _unitOfWork.batteryRepository.GetTotalCountBatteryByUserId(userGetAllBatteryRequestDTO.UserId);


            var pagedResult = new PagedResult<UserBatteryGetAll>
            {
                Items = batteries.ToList(),
                TotalItems = totalItems,
                Page = userGetAllBatteryRequestDTO.Page,
                PageSize = userGetAllBatteryRequestDTO.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalItems / userGetAllBatteryRequestDTO.PageSize)
            };
            if (batteries.Count() == 0)
            {
                return new ResponseDTO<PagedResult<UserBatteryGetAll>>("Can not find any battery", false, null);
            }


            return new ResponseDTO<PagedResult<UserBatteryGetAll>>("Get all batteries successfully", true, pagedResult);
        }

        public async Task<ResponseDTO<UserBatteryDetails>> UserBatteryViewDetailsById(UserViewBatteryDetailsRequestDTO userViewBatteryDetailsRequestDTO)
        {
            var battery = await _unitOfWork.batteryRepository.UserBatteryViewDetailsById(userViewBatteryDetailsRequestDTO.UserId, userViewBatteryDetailsRequestDTO.BatteryId);
            if (battery == null)
            {
                return new ResponseDTO<UserBatteryDetails>("Battery not found or does not belong to the user", false, null);
            }
            return new ResponseDTO<UserBatteryDetails>("Get battery details successfully", true, battery);
        }

        public async Task<ResponseDTO<string>> UserDeleteBattery(int userId, int batteryId)
        {
            var battery = await _unitOfWork.batteryRepository.GetBatteryForUpdate(userId, batteryId);
            if (battery == null)
            {
                return new ResponseDTO<string>("Battery not found or does not belong to the user", false, "Not Found");
            }

            if (battery.Status == "Sold")
            {
                return new ResponseDTO<string>("Cannot delete a sold Battery", false, "Sold");
            }

            //if (car.Status == "Auctioned")
            //{
            //    return new ResponseDTO<string>("Cannot delete an auctioned Battery", false, "Auctioned");
            //}

            if (battery.Status == "Posted")
            {
                return new ResponseDTO<string>("Cannot delete a posted Battery", false, "Posted");
            }

            try
            {
                battery.Status = "Deleted";

                await _unitOfWork.SaveChangesAsync();
                return new ResponseDTO<string>("Battery deleted successfully", true, null);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<string>($"Error deleting Battery: {ex.Message}", false, null);
            }
        }

        public async Task<ResponseDTO<string>> UserUnDeleteBattery(int userId, int carId)
        {
            var battery = await _unitOfWork.batteryRepository.GetBatteryForUpdate(userId, carId);
            if (battery == null)
            {
                return new ResponseDTO<string>("Battery not found or does not belong to the user", false, "Not Found");
            }

            if (battery.Status != "Deleted")
            {
                return new ResponseDTO<string>("Only deleted Batteries can be undeleted", false, $"Battery current status: {battery.Status}");
            }


            try
            {
                battery.Status = "Active";

                await _unitOfWork.SaveChangesAsync();
                return new ResponseDTO<string>("Battery undeleted successfully", true, null);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<string>($"Error undeleting Battery: {ex.Message}", false, null);
            }
        }
    }
}
