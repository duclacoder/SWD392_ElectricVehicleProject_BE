using AutoMapper;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Presentation.RequestModels.UserRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EV.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<ResponseDTO>> LoginUser([FromBody] LoginRequestDTO loginRequestModel)
        {
            if (string.IsNullOrEmpty(loginRequestModel.Email) || string.IsNullOrEmpty(loginRequestModel.Password))
            {
                return new ResponseDTO("Email and password are required", 400, false);
            }
            return await _authService.LoginUser(loginRequestModel);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<ResponseDTO>> Register([FromBody] RegisterRequestDTO registerDTO)
        {
            return Ok(registerDTO);
        }
    }
}
