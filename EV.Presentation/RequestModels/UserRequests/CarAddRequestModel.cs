using System.ComponentModel.DataAnnotations;

namespace EV.Presentation.RequestModels.UserRequests
{
    public class CarAddRequestModel
    {
        [Required]
        public int? UserId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string? VehicleName { get; set; }

        [Required]
        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [StringLength(50)]
        public string? Brand { get; set; }

        [Required]
        [StringLength(50)]
        public string? Model { get; set; }

        [Required]
        [StringLength(30)]
        public string? Color { get; set; }

        [Required]
        [Range(1, 20)]
        public int? Seats { get; set; }

        [Required]
        [StringLength(30)]
        public string? BodyType { get; set; }

        [Required]
        [Range(1, 500)] // kWh
        public decimal? BatteryCapacity { get; set; }

        [Required]
        [Range(1, 2000)] // km
        public int? RangeKm { get; set; }

        [Required]
        [Range(0.1, 48)] // hours
        public decimal? ChargingTimeHours { get; set; }

        [Required]
        public bool? FastChargingSupport { get; set; }

        [Required]
        [Range(1, 2000)] // kW
        public decimal? MotorPowerKw { get; set; }

        [Required]
        [Range(1, 500)] // km/h
        public int? TopSpeedKph { get; set; }

        [Required]
        [Range(0.1, 20)] // 0–100 km/h seconds
        public decimal? Acceleration { get; set; }

        [Required]
        [StringLength(50)]
        public string? ConnectorType { get; set; }

        [Required]
        [Range(1886, 10000)]
        public int? Year { get; set; }

        [Required]
        [Range(0, 1000000)]
        public int? Km { get; set; }

        [Required]
        [StringLength(50)]
        public string? BatteryStatus { get; set; }

        [Required]
        [Range(0, 240)]
        public int? WarrantyMonths { get; set; }

        [Required]
        [Range(1, 100000000)] // reasonable price cap
        public decimal? Price { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Currency must be a 3-letter ISO code (e.g., USD, EUR).")]
        public string? Currency { get; set; }
    }
}
