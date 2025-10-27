using EV.Application.RequestDTOs.AdminRequestDTO;
using EV.Application.RequestDTOs.UserPackagesDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
