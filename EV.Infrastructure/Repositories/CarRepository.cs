using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Domain.CustomEntities;
using EV.Domain.Entities;
using EV.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Infrastructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly Swd392Se1834G2T1Context _context;

        public CarRepository(Swd392Se1834G2T1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserCarGetAll>> GetAllCarByUserId(int id, int skip, int take)
        {
            var cars = await _context.Vehicles.Where(u => u.UserId == id)
                .AsNoTracking()
                .Select(u => new UserCarGetAll
                {
                    UserId = u.UserId,
                    VehiclesId = u.VehiclesId,
                    VehicleName = u.VehicleName,
                    Brand = u.Brand,
                    Model = u.Model,
                    Color = u.Color,
                    Seats = u.Seats,
                    BodyType = u.BodyType,
                    FastChargingSupport = u.FastChargingSupport,
                    Year = u.Year,
                    Km = u.Km,
                    WarrantyMonths = u.WarrantyMonths,
                    Verified = u.Verified,
                    Price = u.Price,
                    Currency = u.Currency,
                    Status = u.Status
                })
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return cars;
        }

        public async Task<int> GetTotalCountCarByUserId(int id)
        {
            return await _context.Vehicles.Where(u => u.UserId == id).CountAsync();
        }

        public async Task<Vehicle?> GetCarForUpdate(int userId, int carId)
        {
            return await _context.Vehicles
                .FirstOrDefaultAsync(v => v.UserId == userId && v.VehiclesId == carId);
        }

        public async Task<UserCarDetails> UserCarViewDetailsById(int userId, int carId)
        {
            var carDetails = await _context.Vehicles
                .AsNoTracking()
                .Select(u => new UserCarDetails
                {
                    UserId = userId,
                    VehiclesId = u.VehiclesId,
                    VehicleName = u.VehicleName,
                    Description = u.Description,
                    Brand = u.Brand,
                    Model = u.Model,
                    Color = u.Color,
                    Seats = u.Seats,
                    BodyType = u.BodyType,
                    BatteryCapacity = u.BatteryCapacity,
                    RangeKm = u.RangeKm,
                    ChargingTimeHours = u.ChargingTimeHours,
                    FastChargingSupport = u.FastChargingSupport,
                    MotorPowerKw = u.MotorPowerKw,
                    TopSpeedKph = u.TopSpeedKph,
                    Acceleration = u.Acceleration,
                    ConnectorType = u.ConnectorType,
                    Year = u.Year,
                    Km = u.Km,
                    BatteryStatus = u.BatteryStatus,
                    WarrantyMonths = u.WarrantyMonths,
                    Price = u.Price,
                    Currency = u.Currency,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,
                    Verified = u.Verified,
                    Status = u.Status
                })
                .FirstOrDefaultAsync(v => v.UserId == userId && v.VehiclesId == carId);
            return carDetails;
        }

        public async Task<AuctionVehicleDetails?> GetAuctionVehicleDetailsById(int carId)
        {
            var vehicles = await _context.Vehicles
                                         .AsNoTracking()
                                         .Include(a => a.VehicleImages)
                                         .Where(a => a.VehiclesId == carId)
                                         .Select(a => new AuctionVehicleDetails
                                         {
                                             VehiclesId = a.VehiclesId,
                                             VehicleName = a.VehicleName,
                                             Brand = a.Brand,
                                             Model = a.Model,
                                             Year = a.Year,
                                             Km = a.Year,
                                             Color = a.Color,
                                             Seats = a.Seats,
                                             BodyType = a.BodyType,
                                             FastChargingSupport = a.FastChargingSupport,
                                             Price = a.Price,
                                             Currency = a.Currency,
                                             Status = a.Status,
                                             WarrantyMonths = a.WarrantyMonths,
                                             VehicleImages = a.VehicleImages.Select(i => i.ImageUrl).ToList()
                                         })
                                         .FirstOrDefaultAsync();
            return vehicles;
        }
    }
}
