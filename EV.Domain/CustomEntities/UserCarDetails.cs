using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Domain.CustomEntities
{
    public class UserCarDetails
    {
        public int VehiclesId { get; set; }

        public int? UserId { get; set; }

        public string? VehicleName { get; set; }

        public string? Description { get; set; }

        public string? Brand { get; set; }

        public string? Model { get; set; }

        public string? Color { get; set; }

        public int? Seats { get; set; }

        public string? BodyType { get; set; }

        public decimal? BatteryCapacity { get; set; }

        public int? RangeKm { get; set; }

        public decimal? ChargingTimeHours { get; set; }

        public bool? FastChargingSupport { get; set; }

        public decimal? MotorPowerKw { get; set; }

        public int? TopSpeedKph { get; set; }

        public decimal? Acceleration { get; set; }

        public string? ConnectorType { get; set; }

        public int? Year { get; set; }

        public int? Km { get; set; }

        public string? BatteryStatus { get; set; }

        public int? WarrantyMonths { get; set; }

        public decimal? Price { get; set; }

        public string? Currency { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? Verified { get; set; }

        public string? Status { get; set; }
    }
}
