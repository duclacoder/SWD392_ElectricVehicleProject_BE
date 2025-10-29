using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.UserRequestDTO
{
    public class UserViewBatteryDetailsRequestDTO
    {
        public int UserId { get; set; }

        public int BatteryId { get; set; }
    }
}
