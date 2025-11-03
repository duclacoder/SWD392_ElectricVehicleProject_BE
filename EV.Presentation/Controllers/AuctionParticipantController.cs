using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.AuctionParticipantDTO;
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

        [HttpGet("Check-eligibility")]
        public async Task<ActionResult<ResponseDTO<bool>>> CheckEligibility(CheckEligibilityRequestDTO request)
        {
            if (request == null)
                return BadRequest(new ResponseDTO<bool>("Input Data is nuill", false));
            if (request.AuctionsId == null)
                return BadRequest(new ResponseDTO<bool>("Auction Id is required", false));
            if (request.UserId == null)
                return BadRequest(new ResponseDTO<bool>("User Id is required", false));


            bool result = await _auctionParticipantService.CheckEligibility(request);
            if (result)
                return Ok(new ResponseDTO<bool>("User can join", true, true));
            else
                return Ok(new ResponseDTO<bool>("User must pay auction Fee", true, false));
        }
    }
}
