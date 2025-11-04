using System;
using System.Collections.Generic;

namespace EV.Domain.Entities;

public partial class User
{
    public int UsersId { get; set; }

    public string? UserName { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Password { get; set; }

    public string? ImageUrl { get; set; }

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public decimal? Wallet { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual ICollection<AuctionBid> AuctionBids { get; set; } = new List<AuctionBid>();

    public virtual ICollection<AuctionParticipant> AuctionParticipants { get; set; } = new List<AuctionParticipant>();

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();

    public virtual ICollection<Battery> Batteries { get; set; } = new List<Battery>();

    public virtual ICollection<BuySell> BuySellBuyers { get; set; } = new List<BuySell>();

    public virtual ICollection<BuySell> BuySellSellers { get; set; } = new List<BuySell>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<UserPackage> UserPackages { get; set; } = new List<UserPackage>();

    public virtual ICollection<UserPost> UserPosts { get; set; } = new List<UserPost>();

    public virtual ICollection<VehicleInspection> VehicleInspections { get; set; } = new List<VehicleInspection>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
