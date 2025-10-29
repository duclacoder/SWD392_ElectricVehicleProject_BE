using AutoMapper;
using Azure;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.PostPackageDTO;
using EV.Application.RequestDTOs.UserPostDTO;
using EV.Application.ResponseDTOs;
using EV.Application.Services;
using EV.Domain.CustomEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EV.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostPackageController : ControllerBase
    {
        private readonly IPostPackageService _postPackageService;
        private readonly IMapper _mapper;

        public PostPackageController(IPostPackageService postPackageService, IMapper mapper)
        {
            _postPackageService = postPackageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPostPackageRequestDTO request)
        {
            var result = await _postPackageService.GetAllPostPackage(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserPostById(int id)
        {
            var result = await _postPackageService.GetPostPackageById(id);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostPackage([FromBody] CreatePostPackageRequestDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _postPackageService.CreatePostPackage(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null
                                                     ? ex.InnerException.Message
                                                     : ex.Message;
                return BadRequest(new ResponseDTO<UserPostCustom>(innerMessage, false));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePostPackage(int id, [FromBody] CreatePostPackageRequestDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _postPackageService.UpdatePostPackage(id, dto);
            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostPackage(int id)
        {
            var result = await _postPackageService.DeletePostPackage(id);
            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string packageName, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(packageName))
                return BadRequest(new ResponseDTO<string>("PackageName is required", false));

            var result = await _postPackageService.SearchPostPackageByPackageName(packageName, page, pageSize);
            return Ok(result);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActive([FromQuery] GetAllPostPackageRequestDTO request)
        {
            if (request.Page <= 0) request.Page = 1;
            if (request.PageSize <= 0) request.PageSize = 10;

            var result = await _postPackageService.GetActivePostPackage(request);
            return Ok(result);
        }

    }
}
