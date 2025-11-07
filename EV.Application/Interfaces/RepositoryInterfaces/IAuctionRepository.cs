using EV.Application.CustomEntities;
using EV.Application.RequestDTOs.AuctionRequestDTO;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IAuctionRepository
    {
        Task<IEnumerable<AuctionCustom>> GetAllAuctions(int skip, int take, string sellerUserName);

        Task<AuctionCustom> GetAuctionById(int id);

        Task<AuctionCustom> CreateAuction(CreateAuctionDTO createAuctionDTO);

        Task<AuctionCustom> UpdateAuction(int id, UpdateAuctionDTO updateAuctionDTO);

        Task<AuctionCustom> DeleteAuction(int id);
    }
}
