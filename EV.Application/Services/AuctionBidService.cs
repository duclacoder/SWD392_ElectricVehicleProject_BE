using EV.Application.CustomEntities;
using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.ResponseDTOs;

namespace EV.Application.Services
{
    public class AuctionBidService : IAuctionBidService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuctionBidService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO<PagedResult<AuctionBidCustom>>> GetAuctionBidByAuctionId(int auctionId)
        {
            try
            {
                var bids = await _unitOfWork.auctionBidRepository.GetAuctionBidByAuctionId(auctionId);

                if (bids == null || !bids.Any())
                {
                    return new ResponseDTO<PagedResult<AuctionBidCustom>>(
                        message: "No bids found for this auction.",
                        isSuccess: false,
                        result: null
                    );
                }

                var resultItems = bids.Select(b => new AuctionBidCustom
                {
                    BidderFullName = b.BidderFullName,
                    BidAmount = b.BidAmount
                }).ToList();

                int page = 1;
                int pageSize = 10;
                int totalItems = resultItems.Count;
                int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                var pagedResult = new PagedResult<AuctionBidCustom>
                {
                    Items = resultItems.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                    TotalItems = totalItems,
                    Page = page,
                    PageSize = pageSize,
                    TotalPages = totalPages
                };

                return new ResponseDTO<PagedResult<AuctionBidCustom>>(
                    message: "Get successfully",
                    isSuccess: true,
                    result: pagedResult
                );
            }
            catch (Exception ex)
            {
                return new ResponseDTO<PagedResult<AuctionBidCustom>>(
                    message: $"Error: {ex.Message}",
                    isSuccess: false,
                    result: null
                );
            }
        }
    }
}
