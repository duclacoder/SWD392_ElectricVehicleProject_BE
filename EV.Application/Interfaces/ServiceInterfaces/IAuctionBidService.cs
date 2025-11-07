using EV.Application.CustomEntities;
using EV.Application.ResponseDTOs;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IAuctionBidService
    {
        Task<ResponseDTO<PagedResult<AuctionBidCustom>>> GetAuctionBidByAuctionId(int auctionId);
    }
}
