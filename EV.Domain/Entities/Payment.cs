﻿using System;
using System.Collections.Generic;

namespace EV.Domain.Entities;

public partial class Payment
{
    public int PaymentsId { get; set; }

    public int UserId { get; set; }

    public int PaymentMethodId { get; set; }

    public string? Gateway { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? AccountNumber { get; set; }

    public string? Content { get; set; }

    public string? TransferType { get; set; }

    public decimal TransferAmount { get; set; }

    public string? Currency { get; set; }

    public decimal? Accumulated { get; set; }

    public string? ReferenceType { get; set; }

    public int? ReferenceId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual ICollection<AuctionParticipant> AuctionParticipants { get; set; } = new List<AuctionParticipant>();

    public virtual PaymentsMethod PaymentMethod { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual UserPackage? UserPackage { get; set; }
}
