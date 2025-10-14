using EV.Application.RequestDTOs.InspectionFeeDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IInspectionFeesService
    {
        Task<ResponseDTO<PagedResult<UserInspectionFeesGetAll>>> UserGetAllInspectionFees(UserGetAllInspectionFeeRequestDTO userGetAllInspectionFeeRequestDTO);
        Task<ResponseDTO<UserGetInspectionFeeById>> UserGetInspectionFeeById(int id);
    }
}
