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
    public class AuctionParticipantRepository : GenericRepository<AuctionParticipant>, IAuctionParticipantRepository
    {
        public AuctionParticipantRepository(Swd392Se1834G2T1Context context) : base(context)
        {
            _context = context;
        }

        public async Task<List<AuctionParticipant>> GetListUserInAuction(int auctionId)
        {
            return await _context.AuctionParticipants
                .Include(p => p.AuctionBids) 
                .Where(p => p.AuctionId == auctionId)
                .ToListAsync();
        }

    }
}
