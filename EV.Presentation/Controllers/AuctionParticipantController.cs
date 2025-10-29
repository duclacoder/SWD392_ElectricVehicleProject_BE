using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.ResponseDTOs;
using EV.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EV.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class AuctionParticipantController : Controller
    {
        private readonly IAuctionParticipantService _auctionParticipantService;

        public AuctionParticipantController(IAuctionParticipantService auctionParticipantService)
        {
            _auctionParticipantService = auctionParticipantService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<ResponseDTO<List<object>>>> GetAllAuctionParticipants()
        {
            var result = await _auctionParticipantService.GetAllAuctionsParticipantAsync();
            return Ok(new ResponseDTO<List<AuctionParticipant>>("Get all successful", true, result));
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ResponseDTO<object>>> CreateAuctionParticipant([FromBody] object auctionParticipantDto)
        {
            //var result = await _auctionParticipantService.CreateAuctionParticipantAsync(auctionParticipantDto);
            return Ok(new ResponseDTO<object>("Creation successful", true));
        }
    }
}
