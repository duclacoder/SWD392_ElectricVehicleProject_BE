using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.AdminRequestDTO;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using EV.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<PagedResult<AdminGetAllUsers>>> GetAllUsers(GetAllUsersRequestDTO getAllUsersRequestDTO)
        {
            var users = await _unitOfWork.userRepository.GetAllUsers((getAllUsersRequestDTO.Page - 1) * getAllUsersRequestDTO.PageSize
                , getAllUsersRequestDTO.PageSize);

            var totalItems = await _unitOfWork.userRepository.GetTotalCountUsers();

            var pagedResult = new PagedResult<AdminGetAllUsers>
            {
                Items = users.ToList(),
                TotalItems = totalItems,
                Page = getAllUsersRequestDTO.Page,
                PageSize = getAllUsersRequestDTO.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalItems / getAllUsersRequestDTO.PageSize)
            };

            if (users.Count() == 0)
            {
                return new ResponseDTO<PagedResult<AdminGetAllUsers>>("Can not find any user", false, null);
            }

            return new ResponseDTO<PagedResult<AdminGetAllUsers>>("Get all users successfully", true, pagedResult);
        }

        public async Task<ResponseDTO<User>> GetUserById(int id)
        {
            var user = await _unitOfWork.userRepository.GetUserById(id);

            if (user == null)
            {
                return new ResponseDTO<User>("User not found", false, null);
            }

            return new ResponseDTO<User>("User found", true, user);
        }

        public async Task<ResponseDTO<User>> CreateUser(CreateUserRequestDTO createUserRequestDTO)
        {
            var user = _mapper.Map<User>(createUserRequestDTO);
            user.CreatedAt = DateTime.Now;
            user.Status = "Active";

            var createdUser = await _unitOfWork.userRepository.CreateUser(user);
            return new ResponseDTO<User>("User created successfully", true, createdUser);
        }

        public async Task<ResponseDTO<User>> UpdateUser(int id, UpdateUserRequestDTO updateUserRequestDTO)
        {
            var user = await _unitOfWork.userRepository.GetUserById(id);

            if (user == null)
            {
                return new ResponseDTO<User>("User not found", false, null);
            }

            user.UserName = updateUserRequestDTO.UserName;
            user.FullName = updateUserRequestDTO.FullName;
            user.Email = updateUserRequestDTO.Email;
            user.Phone = updateUserRequestDTO.Phone;
            user.RoleId = updateUserRequestDTO.RoleId;
            user.Status = updateUserRequestDTO.Status;
            user.UpdatedAt = DateTime.Now;

            var updatedUser = await _unitOfWork.userRepository.UpdateUser(user);
            return new ResponseDTO<User>("User updated successfully", true, updatedUser);
        }

        public async Task<ResponseDTO<bool>> DeleteUser(int id)
        {
            var isDeleted = await _unitOfWork.userRepository.DeleteUser(id);

            if (!isDeleted)
            {
                return new ResponseDTO<bool>("User not found", false, false);
            }

            return new ResponseDTO<bool>("User deleted successfully", true, true);
        }
    }
}
