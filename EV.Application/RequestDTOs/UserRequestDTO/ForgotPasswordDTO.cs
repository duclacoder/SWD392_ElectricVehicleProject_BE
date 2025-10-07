using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.UserRequestDTO
{
    public class ForgotPasswordDTO
    {
        public string? email {  get; set; }
        public string? newPassword { get; set; }
        public string? confirmPassword { get; set; }
    }
}
