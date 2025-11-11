using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.CustomEntities
{
    public class PackageRevenueReportCustom
    {
        public DateTime ReportDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalPackagesSold { get; set; }
        public IEnumerable<PackageRevenueDetailCustom> PackageDetails { get; set; }
    }

    public class PackageRevenueDetailCustom
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; } = string.Empty;
        public decimal PricePerPackage { get; set; }
        public int TotalSold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
