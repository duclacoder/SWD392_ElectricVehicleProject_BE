using System;
using System.Collections.Generic;

namespace EV.Domain.Entities;

public partial class Auction
{
    public int AuctionsId { get; set; }

    public int? VehicleId { get; set; }

    public int? SellerId { get; set; }

    public decimal? StartPrice { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? AuctionsFeeId { get; set; }

    public decimal? FeePerMinute { get; set; }

    public decimal? OpenFee { get; set; }

    public decimal? EntryFee { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<AuctionBid> AuctionBids { get; set; } = new List<AuctionBid>();

    public virtual AuctionsFee? AuctionsFee { get; set; }

    public virtual User? Seller { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
