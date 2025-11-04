using System;
using System.Collections.Generic;

namespace EV.Domain.Models;

public partial class Activity
{
    public int ActivitiesId { get; set; }

    public int? UserId { get; set; }

    public int? PaymentId { get; set; }

    public string? Action { get; set; }

    public int? ReferenceId { get; set; }

    public string? ReferenceType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public virtual Payment? Payment { get; set; }

    public virtual User? User { get; set; }
}
