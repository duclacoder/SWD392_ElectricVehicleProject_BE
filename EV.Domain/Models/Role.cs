using System;
using System.Collections.Generic;

namespace EV.Domain.Models;

public partial class Role
{
    public int RolesId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
