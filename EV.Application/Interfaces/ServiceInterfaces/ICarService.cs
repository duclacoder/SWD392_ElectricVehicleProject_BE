using EV.Application.CustomEntities;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using Microsoft.AspNetCore.Http;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface ICarService
    {
        Task<ResponseDTO<CarAddResponseDTO>> AddCar(CarAddRequestDTO carAddRequestDTO, IFormFile imageUpload);

        Task<ResponseDTO<PagedResult<UserCarGetAll>>> UserCarGetAll(UserGetAllCarRequestDTO userGetAllCarRequestDTO);

        Task<ResponseDTO<UserCarDetails>> UserCarViewDetailsById(UserViewCarDetailsRequestDTO userViewCarDetailsRequestDTO);

        Task<ResponseDTO<string>> UserDeleteCar(int userId, int carId);

        Task<ResponseDTO<string>> UserUnDeleteCar(int userId, int carId);

        Task<ResponseDTO<UserCarUpdateReponse>> UserCarUpdate(UserCarUpdateRequest userCarUpdateRequest, IFormFile imageUpdate);

        Task<ResponseDTO<AuctionVehicleDetails?>> GetCarById(int carId);
    }
}
