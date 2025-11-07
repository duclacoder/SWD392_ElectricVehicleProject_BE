using EV.Application.CustomEntities;
using EV.Application.ResponseDTOs;
using EV.Domain.Entities;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IAuctionBidService
    {
        Task<ResponseDTO<PagedResult<AuctionBidCustom>>> GetAuctionBidByAuctionId(int auctionId);

        Task<User?> GetWinnerByAuctionIdAsync(int auctionId);

    }
}
