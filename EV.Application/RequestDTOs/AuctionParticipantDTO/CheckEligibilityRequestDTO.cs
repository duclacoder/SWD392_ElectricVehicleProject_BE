using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.AuctionParticipantDTO
{
    public class CheckEligibilityRequestDTO
    {
        public int? UserId { get; set; }
        public int? AuctionsId { get; set; }
    }
}
