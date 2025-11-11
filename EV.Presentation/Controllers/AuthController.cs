using AutoMapper;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
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
        private readonly IRedisService _redisService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService, IMapper mapper, IJwtService jwtService, IRedisService redisService, IEmailService emailService)
        {
            _authService = authService;
            _mapper = mapper;
            _jwtService = jwtService;
            _redisService = redisService;
            _emailService = emailService;
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
            if (//string.IsNullOrWhiteSpace(registerDTO.Username) ||
                string.IsNullOrWhiteSpace(registerDTO.Email) ||
                string.IsNullOrWhiteSpace(registerDTO.Password) ||
                //string.IsNullOrWhiteSpace(registerDTO.Phone) ||
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

            var otp = new Random().Next(100000, 999999).ToString();
            await _redisService.StoreDataAsync(registerDTO.Email, otp, TimeSpan.FromMinutes(5));

            var body = await _emailService.LoadTemplateAsync("OtpTemplate.html", new Dictionary<string, string>
            {
                { "OTP_CODE", otp }
            });

            await _emailService.SendMailAsync(registerDTO.Email, "Your OTP Code", body);

            //await _emailService.SendMailAsync(registerDTO.Email, "Your OTP Code", $"Your OTP is: {otp}");

            //var result = await _authService.Register(registerDTO);
            //if (!result.IsSuccess)
            //{
            //    return BadRequest(new ResponseDTO<object>(result.Message, false));
            //}

            //return Ok(new ResponseDTO<object>(result.Message, true, result.Result));
            return Ok(new ResponseDTO<string>("OTP sent, please check your email", true));
        }


        [AllowAnonymous]
        [HttpPost("Confirm_OTP_Register")]
        public async Task<ActionResult<ResponseDTO<string>>> ConfirmOTPRegister([FromBody] RegisterRequestDTO registerDTO, [FromQuery] string otpCode)
        {
            var valid = await _redisService.VerifyDataAsync(registerDTO.Email, otpCode);
            if (!valid)
                return BadRequest(new ResponseDTO<string>("Invalid or expired OTP", false));
            else
            {
                await _redisService.DeleteDataAsync(registerDTO.Email);

                var result = await _authService.Register(registerDTO);
                if (!result.IsSuccess)
                {
                    return BadRequest(new ResponseDTO<object>(result.Message, false));
                }
                return Ok(new ResponseDTO<object>(result.Message, true, result.Result));
            }

            //return Ok(new ResponseDTO<string>("OTP confirmed", true, ""));
        }

        [AllowAnonymous]
        [HttpPost("Resend_OTP")]
        public async Task<ActionResult<ResponseDTO<string>>> ResendOTP([FromQuery] string email)
        {
            var otp = new Random().Next(100000, 999999).ToString();
            await _redisService.StoreDataAsync(email, otp, TimeSpan.FromMinutes(5));
            var body = await _emailService.LoadTemplateAsync("OtpTemplate.html", new Dictionary<string, string>
            {
                { "OTP_CODE", otp }
            });
            await _emailService.SendMailAsync(email, "Your OTP Code", body);

            return Ok(new ResponseDTO<string>("OTP resent", true, ""));
        }


        [AllowAnonymous]
        [HttpPost("Forgot")]
        public async Task<ActionResult<ResponseDTO<object>>> ForgotPassword([FromBody] RegisterRequestDTO registerDTO)
        {
            if (//string.IsNullOrWhiteSpace(registerDTO.Username) ||
                string.IsNullOrWhiteSpace(registerDTO.Email) ||
                string.IsNullOrWhiteSpace(registerDTO.Password) ||
                //string.IsNullOrWhiteSpace(registerDTO.Phone) ||
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

            var otp = new Random().Next(100000, 999999).ToString();
            await _redisService.StoreDataAsync(registerDTO.Email, otp, TimeSpan.FromMinutes(5));

            var body = await _emailService.LoadTemplateAsync("OtpTemplate.html", new Dictionary<string, string>
            {
                { "OTP_CODE", otp }
            });

            await _emailService.SendMailAsync(registerDTO.Email, "Your OTP Code", body);

            //await _emailService.SendMailAsync(registerDTO.Email, "Your OTP Code", $"Your OTP is: {otp}");

            //var result = await _authService.Register(registerDTO);
            //if (!result.IsSuccess)
            //{
            //    return BadRequest(new ResponseDTO<object>(result.Message, false));
            //}
            //return Ok(new ResponseDTO<object>(result.Message, true, result.Result));
            return Ok(new ResponseDTO<string>("OTP sent, please check your email", true));
        }

        [AllowAnonymous]
        [HttpPost("Login-Google")]
        public async Task<ActionResult<ResponseDTO<object>>> LoginGoogle([FromQuery] string? tokenId, [FromBody] GoogleLoginRequest request)
        {
            if (string.IsNullOrEmpty(tokenId))
            {
                return BadRequest(new ResponseDTO<object>("Token ID null", false));
            }

            if (string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new ResponseDTO<object>("Password is required", false));
            }

            if (string.IsNullOrEmpty(request.ConfirmPassword))
            {
                return BadRequest(new ResponseDTO<object>("Confirmed password is required", false));
            }

            if (!request.Password.Equals(request.ConfirmPassword))
            {
                return BadRequest(new ResponseDTO<object>("Confirmed password not match", false));
            }

            return await _authService.LoginGoogle(tokenId, request.Password);

        }
    }
}
