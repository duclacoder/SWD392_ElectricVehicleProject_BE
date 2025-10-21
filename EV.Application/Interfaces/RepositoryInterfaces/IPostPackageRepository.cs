using EV.Application.RequestDTOs.PostPackageDTO;
using EV.Domain.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IPostPackageRepository
    {
        Task<IEnumerable<PostPackageCustom>> GetAllPostPackage(int skip, int take);

        Task<PostPackageCustom> CreatePostPackage(CreatePostPackageRequestDTO createPostPackageRequestDTO);

        Task<PostPackageCustom> UpdatePostPackage(int id,  CreatePostPackageRequestDTO updatePostPackageRequestDTO);

        Task<PostPackageCustom> DeletePostPackage(int id);

        Task<PostPackageCustom> GetPostPackageById(int id);
    }
}
