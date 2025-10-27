using System;
using System.Collections.Generic;

namespace EV.Domain.Entities;

public partial class AuctionParticipant
{
    public int AuctionParticipantId { get; set; }

    public int? PaymentsId { get; set; }

    public int? UserId { get; set; }

    public int? AuctionsId { get; set; }

    public int AuctionId { get; set; }

    public decimal DepositAmount { get; set; }

    public int? PaymentId { get; set; }

    public DateTime? DepositTime { get; set; }

    public string? RefundStatus { get; set; }

    public string? Status { get; set; }

    public bool? IsWinningBid { get; set; }

    public virtual ICollection<AuctionBid> AuctionBids { get; set; } = new List<AuctionBid>();

    public virtual Auction? Auctions { get; set; }

    public virtual Payment? Payments { get; set; }

    public virtual User? User { get; set; }
}
