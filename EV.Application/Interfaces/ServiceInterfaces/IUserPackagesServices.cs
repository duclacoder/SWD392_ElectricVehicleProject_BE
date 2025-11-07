using EV.Application.CustomEntities;
using EV.Application.RequestDTOs.UserPackagesDTO;
using EV.Application.ResponseDTOs;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IUserPackagesServices
    {
        Task<ResponseDTO<PagedResult<UserPackagesCustom>>> GetAllUserPackages(GetAllUserPackageRequestDTO userPackagesRequestDTO);

        Task<ResponseDTO<UserPackagesCustom>> GetUserPackageById(int id);

        Task<ResponseDTO<UserPackagesCustom>> CreateUserPackage(UserPackagesDTO userPackagesDTO);

        Task<ResponseDTO<UserPackagesCustom>> UpdateUserPackage(int id, UserPackagesDTO userPackagesDTO);

        Task<ResponseDTO<UserPackagesCustom>> DeleteUserPackage(int id);

        Task<ResponseDTO<PagedResult<UserPackagesCustom>>> GetUserPackageByUserNameAndPackageName(GetUserPackageByUserNameAndPackageNameRequestDTO request);
        Task<ResponseDTO<UserPackagesCustom>> GetUserPackageByUserId(int id);
    }
}
