using System;
using System.Collections.Generic;

namespace EV.Domain.Models;

public partial class BatteryImage
{
    public int BatteryImagesId { get; set; }

    public int? BatteryId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Battery? Battery { get; set; }
}
