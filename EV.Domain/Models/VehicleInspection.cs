using System;
using System.Collections.Generic;

namespace EV.Domain.Models;

public partial class VehicleInspection
{
    public int VehicleInspectionsId { get; set; }

    public int? VehicleId { get; set; }

    public int? StaffId { get; set; }

    public DateTime? InspectionDate { get; set; }

    public string? Notes { get; set; }

    public string? CancelReason { get; set; }

    public int? InspectionFeeId { get; set; }

    public decimal? InspectionFee { get; set; }

    public string? Status { get; set; }

    public virtual InspectionFee? InspectionFeeNavigation { get; set; }

    public virtual User? Staff { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
