using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<ResponseDTO> LoginUser(LoginRequestDTO loginRequest)
        {
            User loginResult = (User)await _unitOfWork.userRepository.LoginUser(loginRequest);
            
            string token = _jwtService.GenerateToken(loginResult);

            if (loginResult == null)
            {
                return new ResponseDTO("Account does not exist or login information is incorrect", 401, false);
            }
            return new ResponseDTO("Login successful", 200, true, token);
        }
    }
}
