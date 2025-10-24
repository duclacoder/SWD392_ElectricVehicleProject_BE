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
    public interface IBatteryService
    {
        Task<ResponseDTO<PagedResult<UserBatteryGetAll>>> UserBatteryGetAll(UserGetAllBatteryRequestDTO userGetAllBatteryRequestDTO);
    }
}
