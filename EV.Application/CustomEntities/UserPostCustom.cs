using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.CustomEntities
{
    public class UserPostCustom
    {
        public int UserPostId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public VehicleUserPost? Vehicle { get; set; }
        public BatteryUserPost? Battery { get; set; }
        public List<string> Images { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
    }
}
