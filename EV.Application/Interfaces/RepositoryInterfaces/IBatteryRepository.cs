using EV.Domain.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IBatteryRepository
    {
        Task<IEnumerable<UserBatteryGetAll>> GetAllBatteryByUserId(int id, int skip, int take);
        Task<int> GetTotalCountBatteryByUserId(int id);
    }
}
