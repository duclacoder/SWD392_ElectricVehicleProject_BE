using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.AuctionRequestDTO;
using EV.Application.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EV.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionService _auctionService;

        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

         [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAuctionRequestDTO request)
        {
            var response = await _auctionService.GetAllAuction(request);
            return Ok(response); ;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _auctionService.GetAuctionById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuctionDTO dto)
        {
            var response = await _auctionService.CreateAuction(dto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAuctionDTO dto)
        {
            var response = await _auctionService.UpdateAuction(id, dto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _auctionService.DeleteAuction(id);
            return Ok(response);
        }

      
    }
}
