using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.RequestDTOs.UserPackages
{
    public class UserPackagesDTO
    {
       public string UserName { get; set; }

        public string PackagesName { get; set; }

        public int PurchasedDuration { get; set; }

        public decimal PurchasedAtPrice { get; set; }

        public string Currency { get; set; }
    }
}
