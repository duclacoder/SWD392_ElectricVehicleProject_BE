using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Domain.Entities;
using EV.Infrastructure.DBContext;
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
        }
    }
}
