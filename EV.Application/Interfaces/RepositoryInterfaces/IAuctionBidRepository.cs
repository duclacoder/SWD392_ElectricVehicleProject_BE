using EV.Application.CustomEntities;
﻿using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.RepositoryInterfaces
{

    public interface IAuctionBidRepository : IGenericRepository<AuctionBid>
    {
        Task<AuctionBid> GetHighestBid(int auctionId);

        Task<IEnumerable<AuctionBidCustom>> GetAuctionBidByAuctionId(int auctionId);

    }
}
