using System;
using System.ComponentModel.DataAnnotations;

namespace EV.Application.RequestDTOs.AuctionFeeRequestDTO
{
    public class CreateAuctionFeeDTO
    {
        [Required]
        public int? AuctionsId { get; set; }
        public string? Description { get; set; }

        public decimal? FeePerMinute { get; set; }

        public decimal? EntryFee { get; set; }

        public string? Currency { get; set; } = "VND";

        [Required]
        public string? Type { get; set; }

        public string? Status { get; set; } = "Active";
    }
}