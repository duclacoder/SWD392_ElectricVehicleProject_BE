using System;
using System.Collections.Generic;

namespace EV.Domain.Models;

public partial class Vehicle
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

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();

    public virtual ICollection<BuySell> BuySells { get; set; } = new List<BuySell>();

    public virtual User? User { get; set; }

    public virtual ICollection<UserPost> UserPosts { get; set; } = new List<UserPost>();

    public virtual ICollection<VehicleImage> VehicleImages { get; set; } = new List<VehicleImage>();

    public virtual ICollection<VehicleInspection> VehicleInspections { get; set; } = new List<VehicleInspection>();
}
