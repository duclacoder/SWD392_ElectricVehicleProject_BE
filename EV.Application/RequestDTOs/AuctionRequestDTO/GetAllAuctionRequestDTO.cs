using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.AuctionRequestDTO
{
    public class GetAllAuctionRequestDTO
    {
        public string? UserName { get; set; } // filter by seller
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
