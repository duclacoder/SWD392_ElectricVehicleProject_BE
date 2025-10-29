using System;
using System.ComponentModel.DataAnnotations;

namespace EV.Application.RequestDTOs.AuctionFeeRequestDTO
{
    public class UpdateAuctionFeeDTO
    {
        public string? Description { get; set; }

        public decimal? FeePerMinute { get; set; }

        public decimal? EntryFee { get; set; }

        public string? Currency { get; set; }

        public string? Type { get; set; }

        public string? Status { get; set; }
    }
}