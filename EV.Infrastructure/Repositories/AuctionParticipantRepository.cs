using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.RequestDTOs.AuctionParticipantDTO;
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
    public class AuctionParticipantRepository : GenericRepository<AuctionParticipant>, IAuctionParticipantRepository
    {
        private readonly Swd392Se1834G2T1Context _context;
        public AuctionParticipantRepository(Swd392Se1834G2T1Context context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckEligibility(CheckEligibilityRequestDTO request)
        {
            var hasDeposit = await _context.AuctionParticipants
                .AnyAsync(p => p.UserId == request.UserId
                            && p.AuctionsId == request.AuctionsId
                            && p.Status == "Active");
            return hasDeposit;
        }

        public async Task<List<AuctionParticipant>> GetListUserInAuction(int auctionId)
        {
            return await _context.AuctionParticipants
                .Include(p => p.AuctionBids)
                .Where(p => p.AuctionsId == auctionId && p.UserId.HasValue)
                .GroupBy(p => p.UserId)
                .Select(g => g.OrderByDescending(p => p.AuctionParticipantId).FirstOrDefault())
                .ToListAsync();
        }



    }
}
