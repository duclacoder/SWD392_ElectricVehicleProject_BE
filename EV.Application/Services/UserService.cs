using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.AdminRequestDTO;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
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

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
