using System;
using System.Collections.Generic;

namespace EV.Domain.Entities;

public partial class PostPackage
{
    public int PostPackagesId { get; set; }

    public string? PackageName { get; set; }

    public string? Description { get; set; }

    public decimal? PostPrice { get; set; }

    public string? Currency { get; set; }

    public int? PostDuration { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<UserPackage> UserPackages { get; set; } = new List<UserPackage>();
}
