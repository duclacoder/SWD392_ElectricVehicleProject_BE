using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Domain.Entities;
using EV.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace EV.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Swd392Se1834G2T1Context _context;

        public UserRepository(Swd392Se1834G2T1Context context)
        {
            _context = context;
        }

        public async Task<object> LoginUser(LoginRequestDTO loginRequest)
        {
            var user = await _context.Users.Where(c => c.Email == loginRequest.Email && c.Password == loginRequest.Password)
                .Select(u => new
                {
                    UserId = u.UsersId,
                    FullName = u.FullName
                }).FirstOrDefaultAsync();
            if (user == null)
                return null;
            else
                return user;
        }
    }
}
