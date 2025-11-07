using EV.Application.CustomEntities;
using EV.Application.Interfaces.RepositoryInterfaces;
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
    public class InspectionFeesRepository : IInspectionFeesRepository
    {
        private readonly Swd392Se1834G2T1Context _context;

        public InspectionFeesRepository(Swd392Se1834G2T1Context context) => _context = context;

        public async Task<IEnumerable<UserInspectionFeesGetAll>> GetAllInspectionFees(int skip, int take)
        {
            var result = await _context.InspectionFees
                .Select(ifee => new UserInspectionFeesGetAll
                {
                    InspectionFeesId = ifee.InspectionFeesId,
                    Description = ifee.Description,
                    FeeAmount = ifee.FeeAmount,
                    Currency = ifee.Currency,
                    Type = ifee.Type,
                    InspectionDays = ifee.InspectionDays
                })
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return result;
        }

        public async Task<int> GetTotalCountInspectionFees()
        {
            return await _context.InspectionFees.CountAsync();
        }

        public async Task<UserGetInspectionFeeById?> UserGetInspectionFeeById(int id)
        {
            var result = await _context.InspectionFees
                .Where(ifee => ifee.InspectionFeesId == id)
                .Select(ifee => new UserGetInspectionFeeById
                {
                    InspectionFeesId = ifee.InspectionFeesId,
                    Description = ifee.Description,
                    FeeAmount = ifee.FeeAmount,
                    Currency = ifee.Currency,
                    Type = ifee.Type,
                    InspectionDays = ifee.InspectionDays
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
