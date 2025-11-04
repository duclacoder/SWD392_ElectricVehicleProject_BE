using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IAuctionBidService
    {
        Task<ResponseDTO<PagedResult<AuctionBidCustom>>> GetAuctionBidByAuctionId(int auctionId);
    }
}
