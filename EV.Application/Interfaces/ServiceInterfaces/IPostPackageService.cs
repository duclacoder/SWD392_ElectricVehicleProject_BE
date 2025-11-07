using EV.Application.CustomEntities;
using EV.Application.RequestDTOs.PostPackageDTO;
using EV.Application.ResponseDTOs;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IPostPackageService
    {
        Task<ResponseDTO<PagedResult<PostPackageCustom>>> GetAllPostPackage(GetAllPostPackageRequestDTO getAllPostPackageRequestDTO);

        Task<ResponseDTO<PostPackageCustom>> GetPostPackageById(int id);

        Task<ResponseDTO<PostPackageCustom>> CreatePostPackage(CreatePostPackageRequestDTO createPostPackageRequestDTO);

        Task<ResponseDTO<PostPackageCustom>> UpdatePostPackage(int id, CreatePostPackageRequestDTO postPackageCustom);

        Task<ResponseDTO<PostPackageCustom>> DeletePostPackage(int id);

        Task<ResponseDTO<PagedResult<PostPackageCustom>>> GetActivePostPackage(GetAllPostPackageRequestDTO getAllPostPackageRequestDTO);

        Task<ResponseDTO<PagedResult<PostPackageCustom>>> SearchPostPackageByPackageName(string packageName, int page, int pageSize);
    }
}
