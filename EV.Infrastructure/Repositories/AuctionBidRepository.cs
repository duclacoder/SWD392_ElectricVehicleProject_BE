using EV.Application.CustomEntities;
using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Domain.Entities;
using EV.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace EV.Infrastructure.Repositories
{
    public class AuctionBidRepository : GenericRepository<AuctionBid>, IAuctionBidRepository
    {
        public AuctionBidRepository(Swd392Se1834G2T1Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuctionBidCustom>> GetAuctionBidByAuctionId(int auctionId)
        {
            var bids = await _context.AuctionBids
                               .Include(a => a.Bidder)
                               .Where(b => b.AuctionId == auctionId)
                               .Select(b => new AuctionBidCustom
                               {
                                   BidderFullName = b.Bidder.FullName,
                                   BidAmount = b.BidAmount
                               }).ToListAsync();
            return bids;
        }

        public async Task<AuctionBid> GetHighestBid(int auctionId)
        {
            return await _context.AuctionBids
                        .Where(b => b.AuctionId == auctionId && b.Status == "Active")
                        .OrderByDescending(b => b.BidAmount)
                        .ThenBy(b => b.BidTime)
                        .FirstOrDefaultAsync();
        }

        public async Task<User?> GetWinnerByAuctionId(int auctionId)
        {
            var highestBid = await _context.AuctionBids
                .Include(b => b.Bidder)
                .Where(b => b.AuctionId == auctionId && b.Status == "Active")
                .OrderByDescending(b => b.BidAmount)
                .ThenBy(b => b.BidTime)
                .FirstOrDefaultAsync();

            return highestBid?.Bidder;
        }
    }
}
