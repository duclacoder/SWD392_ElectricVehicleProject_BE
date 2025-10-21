using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Domain.CustomEntities
{
    public class PostPackageCustom
    {
        public string? PackageName { get; set; }
        public string? Description { get; set; }
        public decimal? PostPrice { get; set; }
        public string? Currency { get; set; }
        public int? PostDuration { get; set; }
        public string? Status { get; set; }
    }
}
