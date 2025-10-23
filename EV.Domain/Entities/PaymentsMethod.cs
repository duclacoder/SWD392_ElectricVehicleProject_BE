using System;
using System.Collections.Generic;

namespace EV.Domain.Entities;

public partial class PaymentsMethod
{
    public int PaymentMethodId { get; set; }

    public string MethodCode { get; set; } = null!;

    public string MethodName { get; set; } = null!;

    public string? Gateway { get; set; }

    public string? Description { get; set; }

    public string? LogoUrl { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
