using AutoMapper;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Presentation.RequestModels.UserRequests;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EV.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseDTO>> LoginUser([FromBody] LoginRequestDTO loginRequestModel)
        {
            if (string.IsNullOrEmpty(loginRequestModel.Email) || string.IsNullOrEmpty(loginRequestModel.Password))
            {
                return new ResponseDTO("Email and password are required", 400, false);
            }
            return await _userService.LoginUser(loginRequestModel);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> Register([FromBody] RegisterRequestDTO registerDTO)
        {
            return Ok(registerDTO);
        }
    }
}
