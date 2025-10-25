using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserPostDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Services
{
    public class UserPostService : IUserPostsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserPostService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO<UserPostCustom>> CreateUserPost(CreateUserPostDTO createUserPostDTO)
        {
            var result = await _unitOfWork.userPostsRepository.CreateUserPost(createUserPostDTO);

            if(result == null)
            {
                return new ResponseDTO<UserPostCustom>("User not found", false);
            }

            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO<UserPostCustom>("Created successfully", true, result);
        }

        public async Task<ResponseDTO<UserPostCustom>> DeleteUserPost(int id)
        {
            var result = await _unitOfWork.userPostsRepository.DeleteUserPost(id);
            if (result == null)
                return new ResponseDTO<UserPostCustom>("Post not found", false);

            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO<UserPostCustom>("Deleted successfully", true, result);
        }

        public async Task<ResponseDTO<PagedResult<UserPostCustom>>> GetAllUserPosts(GetAllUserPostRequestDTO getAllUserPostRequestDTO)
        {
            var (data, totalItem) = await _unitOfWork.userPostsRepository.GetAllUserPosts(
                                 (getAllUserPostRequestDTO.Page - 1) * getAllUserPostRequestDTO.PageSize,
                                 getAllUserPostRequestDTO.PageSize,
                                 getAllUserPostRequestDTO.UserName);

            var pageResult = new PagedResult<UserPostCustom>
            {
                Items = data.ToList(),
                TotalItems = totalItem,
                Page = getAllUserPostRequestDTO.Page,
                PageSize = getAllUserPostRequestDTO.PageSize,
                TotalPages = (int)Math.Ceiling(totalItem / (double)getAllUserPostRequestDTO.PageSize)
            };

            return new ResponseDTO<PagedResult<UserPostCustom>>("Get successfully", true, pageResult);
        }


        public async Task<ResponseDTO<UserPostCustom>> GetUserPostById(int id)
        {
            var result = await _unitOfWork.userPostsRepository.GetUserPostById(id);
            if (result == null)
            {

                return new ResponseDTO<UserPostCustom>("Post not found", false);
            }

            return new ResponseDTO<UserPostCustom>("Get successfully", true, result);
        }

        public Task<ResponseDTO<UserPostCustom>> UpdateUserPost(int id, UpdateUserPostDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
