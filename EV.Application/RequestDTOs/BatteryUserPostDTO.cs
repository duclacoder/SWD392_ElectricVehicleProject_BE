using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs
{
    public class BatteryUserPostDTO
    {
        public string? BatteryName { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public int? Capacity { get; set; }
        public decimal? Voltage { get; set; }
        public int? WarrantyMonths { get; set; }
        public decimal Price { get; set; }
        public string? Currency { get; set; }
    }
}
