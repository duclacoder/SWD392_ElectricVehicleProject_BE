using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.CustomEntities
{
    public class AuctionReportCustom
    {
        public DateTime ReportDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TotalAuctions { get; set; }
        public int SuccessfulAuctions { get; set; }
        public int FailedAuctions { get; set; }
        public decimal TotalRevenue { get; set; }
        public IEnumerable<AuctionReportDetailCustom> AuctionDetails { get; set; }
    }
 
    public class AuctionReportDetailCustom
    {
        public int AuctionId { get; set; }
        public int VehicleId { get; set; }
        public string SellerName { get; set; } = string.Empty;
        public decimal StartPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public int TotalBids { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string WinnerName { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
    }
}
