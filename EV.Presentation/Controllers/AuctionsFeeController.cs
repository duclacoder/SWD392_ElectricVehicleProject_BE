using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.AuctionFeeRequestDTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EV.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsFeeController : ControllerBase
    {
        private readonly IAuctionsFeeService _auctionsFeeService;

        public AuctionsFeeController(IAuctionsFeeService auctionsFeeService)
        {
            _auctionsFeeService = auctionsFeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAuctionFeeRequestDTO request)
        {
            var response = await _auctionsFeeService.GetAllAuctionFees(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _auctionsFeeService.GetAuctionFeeById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuctionFeeDTO dto)
        {
            var response = await _auctionsFeeService.CreateAuctionFee(dto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAuctionFeeDTO dto)
        {
            var response = await _auctionsFeeService.UpdateAuctionFee(id, dto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _auctionsFeeService.DeleteAuctionFee(id);
            return Ok(response);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Undelete(int id)
        {
            var response = await _auctionsFeeService.UndeleteAuctionFee(id);
            return Ok(response);
        }

        [HttpGet("GetAuctionFee/{auctionId}")]
        public async Task<IActionResult> GetByAuctionFeeByAucionId(int auctionId)
        {
            var response = await _auctionsFeeService.GetAuctionsFeeByAuctionIdAsync(auctionId);
            return Ok(response);
        }
    }
}