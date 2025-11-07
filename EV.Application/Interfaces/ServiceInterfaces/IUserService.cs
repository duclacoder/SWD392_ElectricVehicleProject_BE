using EV.Application.CustomEntities;
using EV.Application.RequestDTOs.AdminRequestDTO;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.Entities;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IUserService
    {
        Task<ResponseDTO<PagedResult<AdminGetAllUsers>>> GetAllUsers(GetAllUsersRequestDTO getAllUsersRequestDTO);
        Task<ResponseDTO<User>> GetUserById(int id);
        Task<ResponseDTO<User>> CreateUser(CreateUserRequestDTO createUserRequestDTO);
        Task<ResponseDTO<User>> UpdateUser(int id, UpdateUserRequestDTO updateUserRequestDTO);
        Task<ResponseDTO<bool>> DeleteUser(int id);
        Task<ResponseDTO<UserProfileUpdate>> UserUpdateProfile(ProfileUpdateRequestDTO profileUpdateRequestDTO);
        Task<ResponseDTO<GetUserProfileById>> GetUserProfileById(int id);
    }
}
