using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Application.ResponseDTOs.UserResponseDTO;
using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<object> LoginUser(LoginRequestDTO loginRequest);
        Task<object> GetAllUsers();
    }
}
