using EV.Application.CustomEntities;
using EV.Domain.Entities;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<UserCarGetAll>> GetAllCarByUserId(int id, int skip, int take);

        Task<int> GetTotalCountCarByUserId(int id);

        Task<UserCarDetails> UserCarViewDetailsById(int userId, int carId);

        Task<Vehicle?> GetCarForUpdate(int userId, int carId);


        Task<AuctionVehicleDetails?> GetAuctionVehicleDetailsById(int carId);
    }
}
