using EV.Application.CustomEntities;
using EV.Application.RequestDTOs.PostPackageDTO;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IPostPackageRepository
    {
        Task<IEnumerable<PostPackageCustom>> GetAllPostPackage(int skip, int take);

        Task<PostPackageCustom> CreatePostPackage(CreatePostPackageRequestDTO createPostPackageRequestDTO);

        Task<PostPackageCustom> UpdatePostPackage(int id, CreatePostPackageRequestDTO updatePostPackageRequestDTO);

        Task<PostPackageCustom> DeletePostPackage(int id);

        Task<PostPackageCustom> GetPostPackageById(int id);

        Task<IEnumerable<PostPackageCustom>> GetActivePostPackage(int skip, int take);

        Task<IEnumerable<PostPackageCustom>> SearchPostPackageByPackageName(string packageName, int skip, int take);
    }
}
