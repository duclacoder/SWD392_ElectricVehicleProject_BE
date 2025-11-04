using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Domain.CustomEntities
{
    public class BatteryUserPost
    {
        public string? BatteryName { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public double Capacity { get; set; }
        public double Voltage { get; set; }
        public int WarrantyMonths { get; set; }
        public decimal Price { get; set; }
        public string? Currency { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Status { get; set; }
    }
}
