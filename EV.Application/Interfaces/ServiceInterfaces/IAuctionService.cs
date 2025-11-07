using EV.Application.CustomEntities;
using EV.Application.RequestDTOs.AuctionRequestDTO;
using EV.Application.ResponseDTOs;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IAuctionService
    {
        Task<ResponseDTO<PagedResult<AuctionCustom>>> GetAllAuction(GetAllAuctionRequestDTO getAllAuctionRequestDTO);

        Task<ResponseDTO<AuctionCustom>> GetAuctionById(int id);

        Task<ResponseDTO<AuctionCustom>> CreateAuction(CreateAuctionDTO createAuctionDTO);

        Task<ResponseDTO<AuctionCustom>> UpdateAuction(int id, UpdateAuctionDTO updateAuctionDTO);

        Task<ResponseDTO<AuctionCustom>> DeleteAuction(int id);
        Task CloseAuction(int auctionId);
    }
}
