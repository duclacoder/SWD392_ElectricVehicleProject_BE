using EV.Application.CustomEntities;
using EV.Application.RequestDTOs.UserPostDTO;
using EV.Application.ResponseDTOs;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IUserPostsService
    {
        Task<ResponseDTO<PagedResult<UserPostCustom>>> GetAllUserPosts(GetAllUserPostRequestDTO getAllUserPostRequestDTO);
        Task<ResponseDTO<UserPostCustom>> GetUserPostById(int id);
        Task<ResponseDTO<UserPostCustom>> CreateUserPost(CreateUserPostDTO createUserPostDTO);
        Task UpdateUserPost(int id, UpdateUserPostDTO dto);
        Task<ResponseDTO<UserPostCustom>> DeleteUserPost(int id);
    }
}
