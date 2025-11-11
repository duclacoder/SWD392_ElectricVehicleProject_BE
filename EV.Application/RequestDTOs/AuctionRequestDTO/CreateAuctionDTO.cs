using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.AuctionRequestDTO
{
    public class CreateAuctionDTO
    {
        public string UserName { get; set; } 
        public int VehicleId { get; set; }

        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; }

        //public int? AuctionsFeeId { get; set; }

        public decimal? FeePerMinute { get; set; }

        public decimal? OpenFee { get; set; }

        public decimal? EntryFee { get; set; }

        public decimal StartPrice { get; set; }

    }
}
