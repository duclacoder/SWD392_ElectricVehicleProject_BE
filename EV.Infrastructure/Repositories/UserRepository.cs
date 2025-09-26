using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Domain.CustomEntities;
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

        public async Task<int> GetTotalCountUsers()
        {
            return await _context.Users.Where(u => u.RoleId != 3).CountAsync();
        }

        public async Task<IEnumerable<AdminGetAllUsers>> GetAllUsers(int skip, int take)
        {
            var users = await _context.Users.Where(u => u.RoleId != 3)
                .AsNoTracking()
                .Select(u => new AdminGetAllUsers
                {
                    UsersId = u.UsersId,
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Email = u.Email,
                    Phone = u.Phone,
                    RoleId = u.RoleId,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,
                    RoleName = u.Role.Name,
                    Status = u.Status
                })
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return users;
        }

        public async Task<User> LoginUser(LoginRequestDTO loginRequest)
        {
            var user = await _context.Users.Where(c => c.Email == loginRequest.Email && c.Password == loginRequest.Password)
                .Select(u => new User
                {
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

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
