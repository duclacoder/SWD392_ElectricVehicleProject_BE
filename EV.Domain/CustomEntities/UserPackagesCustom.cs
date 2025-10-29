using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Domain.CustomEntities
{
    public class UserPackagesCustom
    {
        public required int UserPackagesId { get; set; }

        public string? UserName { get; set; }

        public string? PackagesName { get; set; }

        public int? PurchasedDuration { get; set; }

        public decimal? PurchasedAtPrice { get; set; }

        public string? Currency { get; set; }

        public DateTime? PurchasedAt { get; set; }

        public string Status { get; set; }

        public virtual PostPackage Package { get; set; } = null!;

        public virtual Payment? Payments { get; set; }

        public virtual User User { get; set; } = null!;

        public virtual ICollection<UserPost> UserPosts { get; set; } = new List<UserPost>();
    }
}
