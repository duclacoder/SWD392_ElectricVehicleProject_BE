using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.Entities;

namespace EV.Application.Services
{
    public class AuthService : IAuthService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public AuthService(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<ResponseDTO<object>> IsValidationAccount(RegisterRequestDTO registerDTO)
        {
            ResponseDTO<object> result = await _unitOfWork.authRepository.IsExistAccount(registerDTO);
            return result;
        }
        public async Task<ResponseDTO<string>> LoginUser(LoginRequestDTO loginRequest)
        {
            var loginResult = await _unitOfWork.authRepository.LoginUser(loginRequest);

            if (loginResult == null)
            {
                return new ResponseDTO<string>("Account does not exist or login information is incorrect",false, null);
            }

            string token = _jwtService.GenerateToken(loginResult);
            return new ResponseDTO<string>("Login successful", true, token);
        }

        public async Task<ResponseDTO<bool>> Register(RegisterRequestDTO registerRequest)
        {
            if (await _unitOfWork.authRepository.Register(registerRequest))
                return new ResponseDTO<bool>("Register Successful", true);
            else return new ResponseDTO<bool>("Register fail", false);
        }
    }
}
