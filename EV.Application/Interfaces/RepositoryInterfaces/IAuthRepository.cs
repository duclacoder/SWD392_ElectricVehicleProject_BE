using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IAuthRepository
    {
        Task<User> LoginUser(LoginRequestDTO loginRequest);
        Task<ResponseDTO<object>> IsExistAccount(RegisterRequestDTO accountValidationRequest);
        Task<bool> Register(RegisterRequestDTO registerRequest);
    }
}
