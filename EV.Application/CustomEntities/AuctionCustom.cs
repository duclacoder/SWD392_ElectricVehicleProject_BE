using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.CustomEntities
{
    public class AuctionCustom
    {
        public int AuctionId { get; set; }
        public string SellerUserName { get; set; }
        public int VehicleId { get; set; }
        public decimal StartPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? AuctionsFeeId { get; set; }

        public List<string>? Images { get; set; }


        public decimal? FeePerMinute { get; set; }

        public decimal? OpenFee { get; set; }

        public decimal? EntryFee { get; set; }

        public List<AuctionBidCustom>? Bids { get; set; }

        public string Status { get; set; }
    }
}
