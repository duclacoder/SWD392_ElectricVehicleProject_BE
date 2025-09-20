using System;
using System.Collections.Generic;

namespace EV.Domain.Entities;

public partial class InspectionFee
{
    public int InspectionFeesId { get; set; }

    public string? Description { get; set; }

    public decimal? FeeAmount { get; set; }

    public string? Currency { get; set; }

    public string? Type { get; set; }

    public int? InspectionDays { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<VehicleInspection> VehicleInspections { get; set; } = new List<VehicleInspection>();
}
