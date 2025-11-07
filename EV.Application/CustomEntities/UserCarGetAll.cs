using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.CustomEntities
{
    public class UserCarGetAll
    {
        public int VehiclesId { get; set; }

        public int? UserId { get; set; }

        public string? VehicleName { get; set; }

        public string? Brand { get; set; }

        public string? Model { get; set; }

        public string? Color { get; set; }

        public int? Seats { get; set; }

        public string? BodyType { get; set; }

        public bool? FastChargingSupport { get; set; }

        public int? Year { get; set; }

        public int? Km { get; set; }

        public int? WarrantyMonths { get; set; }

        public decimal? Price { get; set; }

        public string? Currency { get; set; }

        public bool? Verified { get; set; }

        public string? Status { get; set; }

        public string? ImageUrl { get; set; }
    }
}
