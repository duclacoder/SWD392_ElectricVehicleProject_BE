using EV.Application.RequestDTOs.UserPackagesDTO;
using EV.Domain.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IUserPackagesRepository
    {
        Task<IEnumerable<UserPackagesCustom>> GetAllUserPackages(int skip, int take);

        Task<UserPackagesCustom> CreateUserPackage(UserPackagesDTO userPackages);

        Task<UserPackagesCustom> UpdateUserPackage(int id,UserPackagesDTO userPackages);

        Task<UserPackagesCustom> DeleteUserPackage(int id);

        Task<UserPackagesCustom> GetUserPackageById(int id);

        Task<IEnumerable<UserPackagesCustom>> GetUserPackageByUserNameAndPackageName(string userName,string packageName, int skip, int take);
    }
}
