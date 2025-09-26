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
                return Ok(new ResponseDTO<string>("Login successful", true, result.Result));
            }
            return NotFound(new ResponseDTO<string>("Invalid Email or Password", false, null));
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<ResponseDTO<object>>> Register([FromBody] RegisterRequestDTO registerDTO)
        {
            if (string.IsNullOrWhiteSpace(registerDTO.Username) ||
                string.IsNullOrWhiteSpace(registerDTO.Email) ||
                string.IsNullOrWhiteSpace(registerDTO.Password) ||
                string.IsNullOrWhiteSpace(registerDTO.Phone) ||
                string.IsNullOrWhiteSpace(registerDTO.confirmPassword))
            {
                return BadRequest(new ResponseDTO<object>("Thông tin nhập vào không được để trống", false));
            }

            if (registerDTO.Password != registerDTO.confirmPassword)
            {
                return BadRequest(new ResponseDTO<object>("Mật khẩu xác nhận không đúng", false));
            }

            if (!registerDTO.Email.Contains("@"))
            {
                return BadRequest(new ResponseDTO<object>("Invalid email format", false));
            }

            var check = await _authService.IsValidationAccount(registerDTO);
            if (!check.IsSuccess)
            {
                return BadRequest(new ResponseDTO<object>(check.Message, false));
            }

            var result = await _authService.Register(registerDTO);
            if (!result.IsSuccess)
            {
                return BadRequest(new ResponseDTO<object>(result.Message, false));
            }

            return Ok(new ResponseDTO<object>(result.Message, true, result.Result));
        }



        [AllowAnonymous]
        [HttpPost("SendOTP")]
        public async Task<ActionResult<ResponseDTO<string>>> SendOTP([FromQuery] string email)
        {
            return Ok(new ResponseDTO<string>("", true, ""));
        }

        [AllowAnonymous]
        [HttpGet("ConfirmOTP")]
        public async Task<ActionResult<ResponseDTO<string>>> ConfirmOTP([FromQuery] string otpCode)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("ResendOTP")]
        public async Task<ActionResult<ResponseDTO<string>>> ResendOTP()
        {
            return Ok();
        }
    }
}
