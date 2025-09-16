using AutoMapper;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTO.UserRequestDTO;
using EV.Presentation.RequestModels.UserRequests;
using Microsoft.AspNetCore.Mvc;

namespace EV.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> LoginUser([FromBody] LoginRequestModel loginRequestModel)
        {

            var loginRequestDTO = _mapper.Map<LoginRequestDTO>(loginRequestModel);

            var resultLogin = await _userService.LoginUser(loginRequestDTO);

            if (string.IsNullOrEmpty(resultLogin))
            {
                return BadRequest("Invalid username or password");
            }

            return Ok("Login successful");
        }
    }
}
