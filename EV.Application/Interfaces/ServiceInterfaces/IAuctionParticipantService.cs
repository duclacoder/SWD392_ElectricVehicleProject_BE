using EV.Domain.Entities;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IAuctionParticipantService
    {
        Task<bool> IsUserParticipatingInAuction(int userId, int auctionId);
        Task<List<AuctionParticipant>> GetAllAuctionsParticipantAsync();
        Task<AuctionParticipant> GetAuctionParticipantByIdAsync(int auctionParticipantId);
        Task CreateAuctionParticipantAsync(AuctionParticipant auctionParticipant);
        void UpdateAuctionParticipant(AuctionParticipant auctionParticipant);
        Task DeleteAuctionParticipant(int auctionParticipantId);
        Task Refund(int auctionId);
    }
}
