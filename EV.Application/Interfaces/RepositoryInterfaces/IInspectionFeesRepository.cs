using EV.Domain.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IInspectionFeesRepository
    {
        Task<IEnumerable<UserInspectionFeesGetAll>> GetAllInspectionFees(int skip, int take);
        Task<int> GetTotalCountInspectionFees();
        Task<UserGetInspectionFeeById?> UserGetInspectionFeeById(int id);
    }
}
