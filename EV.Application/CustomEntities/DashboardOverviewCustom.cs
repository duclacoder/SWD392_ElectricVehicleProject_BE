using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.CustomEntities
{
    public class DashboardOverviewCustom
    {
        public int TotalUsers { get; set; }
        public int TotalActiveAuctions { get; set; }
        public int TotalActivePosts { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal PackageRevenue { get; set; }
        public decimal AuctionRevenue { get; set; }
    }
}
