using EV.Application.CustomEntities;
using EV.Domain.Entities;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IBatteryRepository
    {
        Task<IEnumerable<UserBatteryGetAll>> GetAllBatteryByUserId(int id, int skip, int take);
        Task<int> GetTotalCountBatteryByUserId(int id);
        Task<Battery?> GetBatteryForUpdate(int userId, int batteryId);
        Task<UserBatteryDetails> UserBatteryViewDetailsById(int userId, int batteryId);
    }
}
