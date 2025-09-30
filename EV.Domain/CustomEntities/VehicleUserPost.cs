using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Domain.CustomEntities
{
    public class VehicleUserPost
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }
        public string Color { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string BodyType { get; set; }

        public int RangeKm { get; set; }

        public int MotorPowerKw { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? Status { get; set; }
    }
}
