using EV.Application.RequestDTOs.PostPackageDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IPostPackageService
    {
        Task<ResponseDTO<PagedResult<PostPackageCustom>>> GetAllPostPackage(GetAllPostPackageRequestDTO getAllPostPackageRequestDTO);

        Task<ResponseDTO<PostPackageCustom>> GetPostPackageById(int id);

        Task<ResponseDTO<PostPackageCustom>> CreatePostPackage(CreatePostPackageRequestDTO createPostPackageRequestDTO);

        Task<ResponseDTO<PostPackageCustom>> UpdatePostPackage(int id,CreatePostPackageRequestDTO postPackageCustom);

        Task<ResponseDTO<PostPackageCustom>> DeletePostPackage(int id);
    }
}
