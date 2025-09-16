using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTO.UserRequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> LoginUser(LoginRequestDTO loginRequest)
        {

            var loginResult = await _unitOfWork.userRepository.LoginUser(loginRequest);

            if (loginResult == null)
            {
                return string.Empty;
            }

            return "Login successful";

        }
    }
}
