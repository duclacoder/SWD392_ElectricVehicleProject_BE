using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.UserPackagesDTO
{
    public class GetUserPackageByUserNameAndPackageNameRequestDTO
    {
        public string UserName { get; set; }

        public string PackageName { get; set; }
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
