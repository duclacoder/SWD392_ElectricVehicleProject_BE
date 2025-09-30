using EV.Application.RequestDTOs.UserPostDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IUserPostsService
    {
        Task<ResponseDTO<PagedResult<UserPostCustom>>> GetAllUserPosts(GetAllUserPostRequestDTO getAllUserPostRequestDTO);
        Task<ResponseDTO<UserPostCustom>> GetUserPostById(int id);
        Task<ResponseDTO<UserPostCustom>> CreateUserPost(CreateUserPostDTO createUserPostDTO);
        Task<ResponseDTO<UserPostCustom>> UpdateUserPost(int id, UpdateUserPostDTO dto);
        Task<ResponseDTO<UserPostCustom>> DeleteUserPost(int id);
    }
}
