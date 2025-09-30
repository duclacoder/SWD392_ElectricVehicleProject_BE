using EV.Domain.CustomEntities;
using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<UserCarGetAll>> GetAllCarByUserId(int id, int skip, int take);

        Task<int> GetTotalCountCarByUserId(int id);

        Task<UserCarDetails> UserCarViewDetailsById(int userId, int carId);

        Task<Vehicle?> GetCarForUpdate(int userId, int carId);
    }
}
