using EV.Application.CustomEntities;
using EV.Domain.Entities;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AdminGetAllUsers>> GetAllUsers(int skip, int take);
        Task<int> GetTotalCountUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<GetUserProfileById?> GetUserProfileById(int id);
    }
}
