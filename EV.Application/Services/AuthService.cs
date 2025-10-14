using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using Google.Apis.Auth;

namespace EV.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly IRedisService _redisService;

        public AuthService(IUnitOfWork unitOfWork, IJwtService jwtService, IEmailService emailService, IRedisService redisService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _emailService = emailService;
            _redisService = redisService;
        }

        public async Task<ResponseDTO<object>> IsValidationAccount(RegisterRequestDTO registerDTO)
        {
            ResponseDTO<object> result = await _unitOfWork.authRepository.IsExistAccount(registerDTO.Email);
            return result;
        }
        public async Task<ResponseDTO<string>> LoginUser(LoginRequestDTO loginRequest)
        {
            var loginResult = await _unitOfWork.authRepository.LoginUser(loginRequest);

            if (loginResult == null)
            {
                return new ResponseDTO<string>("Account does not exist or login information is incorrect", false, null);
            }

            string token = _jwtService.GenerateToken(loginResult);
            return new ResponseDTO<string>("Login successful", true, token);
        }

        public async Task<ResponseDTO<bool>> Register(RegisterRequestDTO registerRequest)
        {
            if (await _unitOfWork.authRepository.Register(registerRequest))
                return new ResponseDTO<bool>("Register Successful", true);
            else return new ResponseDTO<bool>("Register fail", false);
        }

        public async Task<ResponseDTO<object>> ChangePasword(ForgotPasswordDTO forgotPasswordDTO)
        {
            return new ResponseDTO<object>("ok", true);
        }

        public async Task<ResponseDTO<object>> LoginGoogle(string tokenId, string Password)
        {
            var googleData = await GoogleJsonWebSignature.ValidateAsync(tokenId);
            if (googleData == null)
            {
                return new ResponseDTO<object>("Invalid token ID", true);
            }

            var user = await _unitOfWork.userRepository.GetUserByEmail(googleData.Email);

            if (user == null)
            {
                await _unitOfWork.authRepository.GoogleRegister(googleData.Email, Password);

                var otp = new Random().Next(100000, 999999).ToString();
                await _redisService.StoreDataAsync(googleData.Email, otp, TimeSpan.FromMinutes(5));

                var body = await _emailService.LoadTemplateAsync("EmailTemplates/OtpTemplate.html", new Dictionary<string, string>
                {
                    { "OTP_CODE", otp }
                });

                await _emailService.SendMailAsync(googleData.Email, "Your OTP Code", body);
                return new ResponseDTO<object>("OTP sent to your email", true);
            }
            else await _unitOfWork.authRepository.LoginGoogle(googleData.Email);

            return new ResponseDTO<object>("Login google successful", true, user);
        }
    }
}
