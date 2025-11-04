using EV.Application.RequestDTOs.AuctionParticipantDTO;
using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IAuctionParticipantRepository : IGenericRepository<AuctionParticipant>
    {
        Task<bool> CheckEligibility(CheckEligibilityRequestDTO request);
        Task<List<AuctionParticipant>> GetListUserInAuction(int auctionId);
    }
}
