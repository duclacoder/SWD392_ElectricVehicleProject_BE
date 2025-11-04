using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.ResponseDTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EV.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionBidController : ControllerBase
    {
        private readonly IAuctionBidService _auctionBidService;

        public AuctionBidController(IAuctionBidService auctionBidService)
        {
            _auctionBidService = auctionBidService;
        }
        [HttpGet("{auctionId}")]
        public async Task<IActionResult> GetAuctionBidByAuctionId(int auctionId)
        {
            var response = await _auctionBidService.GetAuctionBidByAuctionId(auctionId);

            if (response == null)
            {
                return NotFound(new ResponseDTO<string>("Service returned null", false));
            }

            if (!response.IsSuccess)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
