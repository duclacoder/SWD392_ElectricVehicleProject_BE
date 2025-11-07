using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.CustomEntities
{
    public class UserBatteryGetAll
    {
        public int BatteriesId { get; set; }

        public int? UserId { get; set; }

        public string? BatteryName { get; set; }

        public string? Description { get; set; }

        public string? Brand { get; set; }

        public int? Capacity { get; set; }

        public decimal? Voltage { get; set; }

        public int? WarrantyMonths { get; set; }

        public decimal? Price { get; set; }

        public string? Currency { get; set; }

        public string? Status { get; set; }

        public string? ImageUrl { get; set; }
    }
}
