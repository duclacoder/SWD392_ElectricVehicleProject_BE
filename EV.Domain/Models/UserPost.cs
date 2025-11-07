using System;
using System.Collections.Generic;

namespace EV.Domain.Models;

public partial class UserPost
{
    public int UserPostsId { get; set; }

    public int? UserId { get; set; }

    public int? VehicleId { get; set; }

    public int? BatteryId { get; set; }

    public int? UserPackageId { get; set; }

    public DateTime? PostedAt { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public string? Status { get; set; }

    public virtual Battery? Battery { get; set; }

    public virtual User? User { get; set; }

    public virtual UserPackage? UserPackage { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
