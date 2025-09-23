using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.Entities;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<ResponseDTO<User>> LoginUser(LoginRequestDTO loginRequest);
    }
}
