using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<ResponseDTO> LoginUser(LoginRequestDTO loginRequest);
    }
}
