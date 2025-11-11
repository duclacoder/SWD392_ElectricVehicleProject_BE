using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.CustomEntities
{
    public class PackageStatisticCustom
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; } = string.Empty;
        public decimal PostPrice { get; set; }
        public int TotalSold { get; set; }
        public decimal TotalRevenue { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
