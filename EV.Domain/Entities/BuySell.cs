using System;
using System.Collections.Generic;

namespace EV.Domain.Entities;

public partial class BuySell
{
    public int BuySellId { get; set; }

    public int? BuyerId { get; set; }

    public int? SellerId { get; set; }

    public int? VehicleId { get; set; }

    public int? BatteryId { get; set; }

    public DateTime? BuyDate { get; set; }

    public decimal? CarPrice { get; set; }

    public string? Currency { get; set; }

    public string? Status { get; set; }

    public virtual Battery? Battery { get; set; }

    public virtual User? Buyer { get; set; }

    public virtual User? Seller { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
