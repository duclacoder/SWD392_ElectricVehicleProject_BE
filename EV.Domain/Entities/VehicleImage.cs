using System;
using System.Collections.Generic;

namespace EV.Domain.Entities;

public partial class VehicleImage
{
    public int VehicleImagesId { get; set; }

    public int? VehicleId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
