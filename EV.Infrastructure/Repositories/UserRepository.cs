using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.RequestDTO.UserRequestDTO;
using EV.Application.ResponseDTO.UserResponseDTO;
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

        public async Task<LoginResponseDTO> LoginUser(LoginRequestDTO loginRequest)
        {
            var checkExist = await _context.Set<User>()
                .Where(x => x.Email == loginRequest.Username && x.Password == loginRequest.Password)
                .Select(u => new LoginResponseDTO
                {
                    Userid = u.UserId,
                    Username = u.FullName
                })
                .FirstOrDefaultAsync();


            return checkExist;
        }
    }
}
