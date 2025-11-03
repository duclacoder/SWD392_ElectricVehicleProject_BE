using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.AuctionParticipantDTO
{
    public class CreateAuctionParticipantRequestDTO
    {
        public int AuctionParticipantId { get; set; }

        public int? PaymentsId { get; set; }

        public int? UserId { get; set; }

        public int? AuctionsId { get; set; }

        public decimal DepositAmount { get; set; }

        public DateTime? DepositTime { get; set; }

        public string? RefundStatus { get; set; }

        public string? Status { get; set; }

        public bool? IsWinningBid { get; set; }
    }
}
