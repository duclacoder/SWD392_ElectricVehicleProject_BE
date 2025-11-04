using System;
using System.Collections.Generic;

namespace EV.Domain.Models;

public partial class AuctionBid
{
    public int AuctionBidsId { get; set; }

    public int? AuctionId { get; set; }

    public int? AuctionParticipantId { get; set; }

    public int? BidderId { get; set; }

    public decimal? BidAmount { get; set; }

    public DateTime? BidTime { get; set; }

    public string? Status { get; set; }

    public virtual Auction? Auction { get; set; }

    public virtual AuctionParticipant? AuctionParticipant { get; set; }

    public virtual User? Bidder { get; set; }
}
