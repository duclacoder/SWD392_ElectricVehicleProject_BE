using EV.Application.RequestDTOs.UserPostDTO;
using EV.Domain.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IUserPostsRepository
    {
        Task<(IEnumerable<UserPostCustom> Items, int TotalCount)> GetAllUserPosts(int skip, int take, int? userId);

        Task<UserPostCustom> GetUserPostById(int id);

        Task<UserPostCustom> CreateUserPost(CreateUserPostDTO createUserPostDTO);

        Task<UserPostCustom> UpdateUserPost(int id, UpdateUserPostDTO userPost);

        Task<UserPostCustom> DeleteUserPost(int id);

    }
}
