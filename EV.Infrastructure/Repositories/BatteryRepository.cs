using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Domain.CustomEntities;
using EV.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Infrastructure.Repositories
{
    public class BatteryRepository : IBatteryRepository
    {
        private readonly Swd392Se1834G2T1Context _context;

        public BatteryRepository(Swd392Se1834G2T1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserBatteryGetAll>> GetAllBatteryByUserId(int id, int skip, int take)
        {
            var batteries = await _context.Batteries.Where(u => u.UserId == id)
                .AsNoTracking()
                .Select(u => new UserBatteryGetAll
                {
                    UserId = u.UserId,
                    BatteriesId = u.BatteriesId,
                    BatteryName = u.BatteryName,
                    Description = u.Description,
                    Brand = u.Brand,
                    Capacity = u.Capacity,
                    Voltage = u.Voltage,
                    WarrantyMonths = u.WarrantyMonths,
                    Price = u.Price,
                    Currency = u.Currency,
                    Status = u.Status
                })
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return batteries;
        }

        public async Task<int> GetTotalCountBatteryByUserId(int id)
        {
            return await _context.Batteries.Where(u => u.UserId == id).CountAsync();
        }
    }
}
