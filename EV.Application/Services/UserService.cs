using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
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

        public async Task<ResponseDTO> LoginUser(LoginRequestDTO loginRequest)
        {

            var loginResult = await _unitOfWork.userRepository.LoginUser(loginRequest);

            if (loginResult == null)
            {
                return new ResponseDTO("Account does not exist or login information is incorrect", 401, false);
            } 
                return new ResponseDTO("Login successful", 200, true, loginResult);       
        }

        public async Task<ResponseDTO> GetAllUsers()
        {
            var users = await _unitOfWork.userRepository.GetAllUsers();

            if (users == null)
            {
                return new ResponseDTO("Can not find any user", 404, false);
            }

            return new ResponseDTO("Get all users successfully", 200, true, users);
        }
    }
}
