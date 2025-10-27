using System;
using System.Collections.Generic;

namespace EV.Domain.Entities;

public partial class UserPackage
{
    public int UserPackagesId { get; set; }

    public int UserId { get; set; }

    public int PackageId { get; set; }

    public int? PaymentsId { get; set; }

    public int? PurchasedPostDuration { get; set; }

    public decimal? PurchasedAtPrice { get; set; }

    public string? Currency { get; set; }

    public DateTime? PurchasedAt { get; set; }

    public string? Status { get; set; }

    public virtual PostPackage Package { get; set; } = null!;

    public virtual Payment? Payments { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserPost> UserPosts { get; set; } = new List<UserPost>();
}
