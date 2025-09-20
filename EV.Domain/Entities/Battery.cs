using System;
using System.Collections.Generic;

namespace EV.Domain.Entities;

public partial class Battery
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

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<BatteryImage> BatteryImages { get; set; } = new List<BatteryImage>();

    public virtual ICollection<BuySell> BuySells { get; set; } = new List<BuySell>();

    public virtual User? User { get; set; }

    public virtual ICollection<UserPost> UserPosts { get; set; } = new List<UserPost>();
}
