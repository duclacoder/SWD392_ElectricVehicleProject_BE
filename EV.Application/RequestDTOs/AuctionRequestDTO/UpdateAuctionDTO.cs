using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.AuctionRequestDTO
{
    public class UpdateAuctionDTO
    {
        public DateTime? EndTime { get; set; }
        public string Status { get; set; }
    }
}
