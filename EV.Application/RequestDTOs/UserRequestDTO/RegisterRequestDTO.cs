using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.UserRequestDTO
{
    public class RegisterRequestDTO
    {
        //public required string Username {get; set;}
        public required string Email { get; set; }
        public required string Password { get; set; }
        //public required string Phone { get; set; }
        public required string confirmPassword { get; set; }
    }
}
