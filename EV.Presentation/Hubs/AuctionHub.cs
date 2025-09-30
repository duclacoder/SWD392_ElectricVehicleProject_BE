using EV.Application.Interfaces.HubsInterfaces;
using EV.Domain.Entities;
using EV.Infrastructure.DBContext;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace EV.Presentation.Hubs
{
    public class AuctionHub : Hub, IAuctionsHubs
    {
        private readonly Swd392Se1834G2T1Context _context;

        public AuctionHub(Swd392Se1834G2T1Context context) => _context = context;

        public async Task SendBid(int auctionId, int bidderId, decimal bidderAmount)
        {
            //await Clients.Group($"auction_{auctionId}")
            //             .SendAsync("ReceiveBid", auctionId, bidderId, bidderAmount);
            var auction = await _context.Auctions.FirstOrDefaultAsync(b => b.AuctionsId == auctionId);

            if(auction == null)
            {
                await Clients.Caller.SendAsync("BidRejected", "Auction not found !");
                return;
            }

            var currentPrice = await _context.AuctionBids.Where(b => b.AuctionId == auctionId).Select(b => b.BidAmount).DefaultIfEmpty(auction.StartPrice).MaxAsync();
            
            if(bidderAmount <= currentPrice)
            {
                await Clients.Caller.SendAsync("BidRejected", $"Bid must be higher than {bidderAmount}");
                return;
            }
            var newBid = new AuctionBid
            {
                AuctionId = auctionId,
                BidderId = bidderId,
                BidAmount = bidderAmount,
                BidTime = DateTime.Now,
                Status = "Active"
            };

            _context.AuctionBids.Add(newBid);
            await _context.SaveChangesAsync();

            await Clients.Groups($"auction_{auctionId}")
                .SendAsync("ReciveBid", auctionId, bidderId, bidderAmount);
        }

        public async Task JoinAuction(int auctionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"auction_{auctionId}");
        }

        public async Task LeaveAuction(int auctionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"auction_{auctionId}");
        }
    }
}
