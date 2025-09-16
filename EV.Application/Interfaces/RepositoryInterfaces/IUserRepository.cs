using EV.Application.RequestDTO.UserRequestDTO;
using EV.Application.ResponseDTO.UserResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<LoginResponseDTO> LoginUser(LoginRequestDTO loginRequest);
    }
}
