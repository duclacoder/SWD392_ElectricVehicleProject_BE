using AutoMapper;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EV.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;


        public AuthController(IAuthService authService, IMapper mapper, IJwtService jwtService)
        {
            _authService = authService;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<ResponseDTO<string>>> LoginUser([FromBody] LoginRequestDTO loginRequestModel)
        {
            if (string.IsNullOrEmpty(loginRequestModel.Email) || string.IsNullOrEmpty(loginRequestModel.Password))
            {
                return BadRequest(new ResponseDTO<string>("Email and password are required", false, null));
            }

            var result = await _authService.LoginUser(loginRequestModel);

            if (result.Result != null)
            {
                string token = _jwtService.GenerateToken(result.Result);
                return Ok(new ResponseDTO<string>("Login successful", true, token));
            }

            return NotFound(new ResponseDTO<string>("Invalid Email or Password", false, null));
        }



        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequestDTO registerDTO)
        {
            return Ok(registerDTO);
        }
    }
}
