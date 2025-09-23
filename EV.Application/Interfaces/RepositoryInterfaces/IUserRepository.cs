using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Domain.CustomEntities;
using EV.Domain.Entities;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<User> LoginUser(LoginRequestDTO loginRequest);
        Task<IEnumerable<AdminGetAllUsers>> GetAllUsers(int skip, int take);
        Task<int> GetTotalCountUsers();
    }
}
