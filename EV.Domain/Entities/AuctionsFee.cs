using System;
using System.Collections.Generic;

namespace EV.Domain.Entities;

public partial class AuctionsFee
{
    public int AuctionsFeeId { get; set; }

    public string? Description { get; set; }

    public decimal? FeePerMinute { get; set; }

    public decimal? EntryFee { get; set; }

    public string? Currency { get; set; }

    public string? Type { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();
}
