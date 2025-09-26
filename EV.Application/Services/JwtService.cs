using EV.Application.Interfaces.ServiceInterfaces;
using EV.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EV.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expireMinutes;

        public JwtService(IConfiguration config)
        {
            _key = config["Jwt:Key"];
            _issuer = config["Jwt:Issuer"];
            _audience = config["Jwt:Audience"];
            _expireMinutes = int.Parse(config["Jwt:ExpireMinutes"]);
        }

        public string GenerateToken(User user)
        {
            if (user == null) return null;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UsersId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("fullName", user.FullName),
                new Claim("userName", user.UserName),
                new Claim("imageUrl", user.ImageUrl ?? string.Empty),
                new Claim("phone", user.Phone ?? string.Empty),
                new Claim("role", user.Role.Name),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_expireMinutes),
                signingCredentials: credentials
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
