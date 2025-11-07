using EV.Application.CustomEntities;
using EV.Application.RequestDTOs.InspectionFeeDTO;
using EV.Application.ResponseDTOs;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IInspectionFeesService
    {
        Task<ResponseDTO<PagedResult<UserInspectionFeesGetAll>>> UserGetAllInspectionFees(UserGetAllInspectionFeeRequestDTO userGetAllInspectionFeeRequestDTO);
        Task<ResponseDTO<UserGetInspectionFeeById>> UserGetInspectionFeeById(int id);
    }
}
