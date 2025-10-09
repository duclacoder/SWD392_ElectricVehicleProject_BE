using System;
using System.Collections.Generic;

namespace EV.Domain.Entities;

public partial class Role
{
    public int RolesId { get; set; }

    public required string Name { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
