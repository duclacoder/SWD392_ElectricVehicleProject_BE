using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Domain.Entities;
using EV.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Infrastructure.Repositories
{
    public class AuctionBidRepository : GenericRepository<AuctionBid>, IAuctionBidRepository
    {
        public AuctionBidRepository(Swd392Se1834G2T1Context context) : base(context)
        {
            _context = context;
        }

        public async Task<AuctionBid> GetHighestBid(int auctionId)
        {
            return await _context.AuctionBids
                        .Where(b => b.AuctionId == auctionId && b.Status == "Valid")
                        .OrderByDescending(b => b.BidAmount)
                        .ThenBy(b => b.BidTime)
                        .FirstOrDefaultAsync();
        }
    }
}
