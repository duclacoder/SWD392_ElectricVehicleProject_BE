using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.HubsInterfaces
{
    public interface IAuctionsHubs
    {
        public Task SendBid(int auctionId, int bidderId, decimal bidderAmount);

        public Task JoinAuction(int auctionId);

        public Task LeaveAuction(int auctionId);
    }
}
