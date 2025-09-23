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
        //private readonly IJwtService _jwtService;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<ResponseDTO<User>> LoginUser(LoginRequestDTO loginRequest)
        {
            User loginResult = (User)await _unitOfWork.userRepository.LoginUser(loginRequest);


            //string token = _jwtService.GenerateToken(loginResult);

            if (loginResult == null)
            {
                return new ResponseDTO<User>("Account does not exist or login information is incorrect", false, null);
            }
            return new ResponseDTO<User>("Login successful", true, loginResult);
        }
    }
}
