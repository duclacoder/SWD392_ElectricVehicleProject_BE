using AutoMapper;
using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.PostPackageDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Services
{
    public class PostPackagesService : IPostPackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostPackagesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<PostPackageCustom>> CreatePostPackage(CreatePostPackageRequestDTO createPostPackageRequestDTO)
        {
            var result = await _unitOfWork.postPackageRepository.CreatePostPackage(createPostPackageRequestDTO);
            if(result == null)
            {
                return new ResponseDTO<PostPackageCustom>("Post package is not found", false);
            }

            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO<PostPackageCustom>("Create successfully", true, result);
        }

        public async Task<ResponseDTO<PostPackageCustom>> DeletePostPackage(int id)
        {
            var result = await _unitOfWork.postPackageRepository.DeletePostPackage(id);
            if (result == null)
            {
                return new ResponseDTO<PostPackageCustom>("Delete Post Package Fail", false);
            }
            
            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO<PostPackageCustom>("Delete successfully", true, result);
        }

        public async Task<ResponseDTO<PagedResult<PostPackageCustom>>> GetAllPostPackage(GetAllPostPackageRequestDTO getAllPostPackageRequestDTO)
        {
            var result = await _unitOfWork.postPackageRepository.GetAllPostPackage((getAllPostPackageRequestDTO.Page - 1) *  getAllPostPackageRequestDTO.PageSize, getAllPostPackageRequestDTO.PageSize);

            var totalItem = result.Count();
            var pageResult = new PagedResult<PostPackageCustom>
            {
                Items = result.ToList(),
                TotalItems = totalItem,
                Page = getAllPostPackageRequestDTO.Page,
                PageSize = getAllPostPackageRequestDTO.PageSize,
                TotalPages = (int)Math.Ceiling(totalItem / (double)getAllPostPackageRequestDTO.PageSize)
            };

            return new ResponseDTO<PagedResult<PostPackageCustom>>("Get successfully", true, pageResult);
        }

        public async Task<ResponseDTO<PostPackageCustom>> GetPostPackageById(int id)
        {
            var result = await _unitOfWork.postPackageRepository.GetPostPackageById(id);

            if(result == null)
            {
                return new ResponseDTO<PostPackageCustom>("Post Package is not found", false);
            }

            return new ResponseDTO<PostPackageCustom>($"Get post {id} successfully", true, result);

        }

        public async Task<ResponseDTO<PostPackageCustom>> UpdatePostPackage(int id, CreatePostPackageRequestDTO postPackageCustom)
        {
            var result = await _unitOfWork.postPackageRepository.UpdatePostPackage(id, postPackageCustom);

            if(result == null)
            {
                return new ResponseDTO<PostPackageCustom>("Post package is not found", false);
            }
            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO<PostPackageCustom>("Update succesfully", true, result);
        }
    }
}
