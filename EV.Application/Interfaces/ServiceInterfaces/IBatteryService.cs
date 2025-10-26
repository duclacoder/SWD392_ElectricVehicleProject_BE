using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IBatteryService
    {
        Task<ResponseDTO<BatteryAddResponseDTO>> AddBattery(BatteryAddRequestDTO batteryAddRequestDTO, IFormFile imageUpload);

        Task<ResponseDTO<PagedResult<UserBatteryGetAll>>> UserBatteryGetAll(UserGetAllBatteryRequestDTO userGetAllBatteryRequestDTO);

        Task<ResponseDTO<string>> UserDeleteBattery(int userId, int batteryId);

        Task<ResponseDTO<string>> UserUnDeleteBattery(int userId, int batteryId);

        Task<ResponseDTO<UserBatteryDetails>> UserBatteryViewDetailsById(UserViewBatteryDetailsRequestDTO userViewBatteryDetailsRequestDTO);
    }
}
