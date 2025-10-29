using AutoMapper;
using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserPackagesDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EV.Application.Services
{
    public class UserPackagesService : IUserPackagesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserPackagesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseDTO<UserPackagesCustom>> CreateUserPackage(UserPackagesDTO userPackagesDTO)
        {
            var result = await _unitOfWork.userPackagesRepository.CreateUserPackage(userPackagesDTO);
            if (result == null)
            {
                return new ResponseDTO<UserPackagesCustom>("User or Package not found", false);
            }
            
            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO<UserPackagesCustom>("Created successfully", true, result);

        }

        public async Task<ResponseDTO<UserPackagesCustom>> DeleteUserPackage(int id)
        {
            var result = await _unitOfWork.userPackagesRepository.DeleteUserPackage(id);
            if(result == null)
            {
                return new ResponseDTO<UserPackagesCustom>("UserPackage not found", false);
            }
            
            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO<UserPackagesCustom>("Deleted successfully", true, result);

        }

        public async Task<ResponseDTO<PagedResult<UserPackagesCustom>>> GetAllUserPackages(GetAllUserPackageRequestDTO userPackagesRequestDTO)
        {
            var data = await _unitOfWork.userPackagesRepository.GetAllUserPackages((userPackagesRequestDTO.Page - 1) * userPackagesRequestDTO.PageSize, userPackagesRequestDTO.PageSize);

            var totalItem = data.Count();
            var pageResult = new PagedResult<UserPackagesCustom>
            {
                Items = data.ToList(),
                TotalItems = totalItem,
                Page = userPackagesRequestDTO.Page,
                PageSize = userPackagesRequestDTO.PageSize,
                TotalPages = (int)Math.Ceiling(totalItem / (double)userPackagesRequestDTO.PageSize)
            };

            return new ResponseDTO<PagedResult<UserPackagesCustom>>("Get successfully", true, pageResult);

        }

        public async Task<ResponseDTO<UserPackagesCustom>> GetUserPackageById(int id)
        {
            var result = await _unitOfWork.userPackagesRepository.GetUserPackageById(id);

            if (result == null)
                return new ResponseDTO<UserPackagesCustom>("User package not found", false);

            return new ResponseDTO<UserPackagesCustom>("Get successfully", true, result);
        }

        public async Task<ResponseDTO<UserPackagesCustom>> GetUserPackageByUserId(int id)
        {
            var result = await _unitOfWork.userPackagesRepository.GetUserPackageByUserId(id);
            if (result == null)
                return new ResponseDTO<UserPackagesCustom>("User package not found", false);
            return new ResponseDTO<UserPackagesCustom>("Get successfully", true, result);
        }

        public async Task<ResponseDTO<PagedResult<UserPackagesCustom>>> GetUserPackageByUserNameAndPackageName(GetUserPackageByUserNameAndPackageNameRequestDTO request)
        {
            var x = await _unitOfWork.userPackagesRepository.GetUserPackageByUserNameAndPackageName(request.UserName, request.PackageName, (request.Page - 1) * request.PageSize, request.PageSize);

            var totalItem = x.Count();

            var pageResult = new PagedResult<UserPackagesCustom>
            {
                Items = x.ToList(),
                TotalItems = totalItem,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalItem / (double)request.PageSize)
            };

            return new ResponseDTO<PagedResult<UserPackagesCustom>>("Get successfully", true, pageResult);
        }

        public async Task<ResponseDTO<UserPackagesCustom>> UpdateUserPackage(int id, UserPackagesDTO userPackagesDTO)
        {
            var result = await _unitOfWork.userPackagesRepository.UpdateUserPackage(id, userPackagesDTO);
            if (result == null)
            {
                return new ResponseDTO<UserPackagesCustom>("UserPackage not found", false);
            }

            await _unitOfWork.SaveChangesAsync();

            //var updated = new UserPackagesCustom
            //{
            //    PackagesName = userPackagesDTO.PackagesName,
            //    PurchasedDuration = userPackagesDTO.PurchasedDuration,
            //    PurchasedAtPrice = userPackagesDTO.PurchasedAtPrice,
            //    Currency = userPackagesDTO.Currency,
            //    Status = "Active", 
            //    PurchasedAt = DateTime.Now
            //};

            return new ResponseDTO<UserPackagesCustom>("Updated successfully", true, result);
        }

    }
}
