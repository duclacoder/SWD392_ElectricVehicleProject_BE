using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.CustomEntities
{
    public class RevenueByTimeCustom
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal PackageRevenue { get; set; }
        public decimal AuctionRevenue { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
