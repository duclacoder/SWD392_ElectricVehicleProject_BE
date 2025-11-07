using EV.Application.CustomEntities;
using EV.Application.RequestDTOs.UserPostDTO;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IUserPostsRepository
    {
        Task<(IEnumerable<UserPostCustom> Items, int TotalCount)> GetAllUserPosts(int skip, int take, int? userId, bool? isVehiclePost = null);

        Task<UserPostCustom> GetUserPostById(int id);

        Task<UserPostCustom> CreateUserPost(CreateUserPostDTO createUserPostDTO);

        Task UpdateUserPost(int id, UpdateUserPostDTO userPost);

        Task<UserPostCustom> DeleteUserPost(int id);

    }
}
