using AutoMapper;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserPostDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EV.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPostController : ControllerBase
    {
        private readonly IUserPostsService _userPostsService;
        private readonly IMapper _mapper;

        public UserPostController(IUserPostsService userPostsService, IMapper mapper)
        {
            _mapper = mapper;
            _userPostsService = userPostsService; 
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllUserPostRequestDTO request)
        {
            var result = await _userPostsService.GetAllUserPosts(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserPostById(int id)
        {
            var result = await _userPostsService.GetUserPostById(id);
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserPost([FromForm] CreateUserPostDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _userPostsService.CreateUserPost(dto);
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
        public async Task<IActionResult> UpdateUserPost(int id, [FromBody] UpdateUserPostDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _userPostsService.UpdateUserPost(id, dto);
                return Ok(new { message = "Cập nhật bài đăng thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPost(int id)
        {
            var result = await _userPostsService.DeleteUserPost(id);
            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }
    }
}
