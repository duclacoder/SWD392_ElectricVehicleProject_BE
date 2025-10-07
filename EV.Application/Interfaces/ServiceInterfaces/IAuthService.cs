using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.Entities;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<ResponseDTO<string>> LoginUser(LoginRequestDTO loginRequest);
        Task<ResponseDTO<object>> IsValidationAccount(RegisterRequestDTO accountValidationRequest); 
        Task<ResponseDTO<bool>> Register(RegisterRequestDTO registerRequest);
        Task<ResponseDTO<object>> ChangePasword(ForgotPasswordDTO forgotPasswordDTO);
        Task<ResponseDTO<object>> LoginGoogle(string tokenId, string Password);
    }
}
