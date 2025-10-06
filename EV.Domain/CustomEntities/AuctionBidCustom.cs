using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Domain.CustomEntities
{
    public class AuctionBidCustom
    {
        public int AuctionBidId { get; set; }
        public string? BidderUserName { get; set; }
        public decimal? BidAmount { get; set; }
        public DateTime? BidTime { get; set; }
    }
}
