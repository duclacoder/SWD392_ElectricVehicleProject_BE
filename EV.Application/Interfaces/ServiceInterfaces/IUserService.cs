using EV.Application.RequestDTO.UserRequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IUserService
    {
        Task<string> LoginUser(LoginRequestDTO loginRequest);
    }
}
