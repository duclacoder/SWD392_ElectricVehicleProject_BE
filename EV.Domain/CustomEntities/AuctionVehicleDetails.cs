using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Domain.CustomEntities
{
    public class AuctionVehicleDetails
    {
        public int VehiclesId { get; set; }
        public string? VehicleName { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public int? Km { get; set; }
        public string? Color { get; set; }
        public int? Seats { get; set; }
        public string? BodyType { get; set; }
        public bool? FastChargingSupport { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public string? Status { get; set; }
        public int? WarrantyMonths { get; set; }
        public string? BatteryStatus { get; set; }
        public List<string>? VehicleImages { get; set; }
    }
}
