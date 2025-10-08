using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.Entities;
using EV.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace EV.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly Swd392Se1834G2T1Context _context;

        public AuthRepository(Swd392Se1834G2T1Context context)
        {
            _context = context;
        }

        public async Task<bool> GoogleRegister(string email, string password)
        {
            User registerUser = new User()
            {
                Email = email,
                Password = password,
                RoleId = 1,
            };
            if (registerUser == null) return false;
            else
            {
                await _context.Users.AddAsync(registerUser);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<ResponseDTO<object>> IsExistAccount(string email)
        {
            var user = await _context.Users.Where(u => u.Email == email).Select(u => u.Email).FirstOrDefaultAsync();
            if (user != null)
            {
                return new ResponseDTO<object>("Email của bạn đã được sử dụng", false);
            }

            return new ResponseDTO<object>("Tài khoản hợp lệ để đăng ký", true);
        }

        public async Task<User> LoginGoogle(string Email)
        {
            var user = await _context.Users.Where(c => c.Email == Email)
            .Select(u => new User
            {
                UsersId = u.UsersId,
                UserName = u.UserName,
                FullName = u.FullName,
                Email = u.Email,
                ImageUrl = u.ImageUrl,
                Role = u.Role,
            }).FirstOrDefaultAsync();
            if (user == null)
                return null;
            else
                return user;
        }

        public async Task<User> LoginUser(LoginRequestDTO loginRequest)
        {
            var user = await _context.Users.Where(c => c.Email == loginRequest.Email && c.Password == loginRequest.Password)
                .Select(u => new User
                {
                    UsersId = u.UsersId,
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Email = u.Email,
                    ImageUrl = u.ImageUrl,
                    Role = u.Role,
                }).FirstOrDefaultAsync();
            if (user == null)
                return null;
            else
                return user;
        }

        public async Task<bool> Register(RegisterRequestDTO registerRequest)
        {
            User registerUser = new User()
            {
                Email = registerRequest.Email,
                Password = registerRequest.Password,
                RoleId = 1,
            };
            if (registerUser == null) return false;
            else
            {
                await _context.Users.AddAsync(registerUser);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
