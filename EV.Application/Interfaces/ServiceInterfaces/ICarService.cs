using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface ICarService
    {
        Task<ResponseDTO<CarAddResponseDTO>> AddCar(CarAddRequestDTO carAddRequestDTO);

        Task<ResponseDTO<PagedResult<UserCarGetAll>>> UserCarGetAll(UserGetAllCarRequestDTO userGetAllCarRequestDTO);

        Task<ResponseDTO<UserCarDetails>> UserCarViewDetailsById(UserViewCarDetailsRequestDTO userViewCarDetailsRequestDTO);

        Task<ResponseDTO<string>> UserDeleteCar(int userId, int carId);

        Task<ResponseDTO<string>> UserUnDeleteCar(int userId, int carId);

        Task<ResponseDTO<UserCarUpdateReponse>> UserCarUpdate(UserCarUpdateRequest userCarUpdateRequest);
    }
}
