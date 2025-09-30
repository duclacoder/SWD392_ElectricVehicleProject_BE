using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.UserRequestDTO
{
    public class UserViewCarDetailsRequestDTO
    {
        public int UserId { get; set; }

        public int VehicleId { get; set; }
    }
}
