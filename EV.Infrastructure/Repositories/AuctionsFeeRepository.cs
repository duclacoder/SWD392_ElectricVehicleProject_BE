using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.RequestDTOs.AuctionFeeRequestDTO;
using EV.Domain.Entities;
using EV.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EV.Infrastructure.Repositories
{
    public class AuctionsFeeRepository : IAuctionsFeeRepository
    {
        private readonly Swd392Se1834G2T1Context _context;

        public AuctionsFeeRepository(Swd392Se1834G2T1Context context)
        {
            _context = context;
        }

        public async Task<AuctionsFee> CreateAsync(CreateAuctionFeeDTO dto)
        {
            var auctionFee = new AuctionsFee
            {
                AuctionsId = dto.AuctionsId,
                Description = dto.Description,
                FeePerMinute = dto.FeePerMinute,
                EntryFee = dto.EntryFee,
                Currency = dto.Currency,
                Type = dto.Type,
                Status = dto.Status,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.AuctionsFees.Add(auctionFee);
            // SaveChangesAsync sẽ được gọi ở UnitOfWork trong Service
            return auctionFee;
        }

        public async Task<AuctionsFee?> DeleteAsync(int id)
        {
            var auctionFee = await _context.AuctionsFees.FindAsync(id);
            if (auctionFee == null)
            {
                return null;
            }

            auctionFee.Status = "InActive";
            auctionFee.UpdatedAt = DateTime.Now;
            _context.AuctionsFees.Update(auctionFee);
            return auctionFee;
        }

        public async Task<AuctionsFee?> UndeleteAsync(int id)
        {
            var auctionFee = await _context.AuctionsFees.FindAsync(id);
            if (auctionFee == null)
            {
                return null;
            }

            auctionFee.Status = "Active";
            auctionFee.UpdatedAt = DateTime.Now;
            _context.AuctionsFees.Update(auctionFee);
            return auctionFee;
        }

        public async Task<IEnumerable<AuctionsFee>> GetAllAsync(int skip, int take, string? status, string? type)
        {
            var query = _context.AuctionsFees.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.Status == status);
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(x => x.Type == type);
            }

            return await query.OrderByDescending(x => x.CreatedAt)
                              .Skip(skip)
                              .Take(take)
                              .ToListAsync();
        }

        public async Task<AuctionsFee?> GetByIdAsync(int id)
        {
            return await _context.AuctionsFees.FindAsync(id);
        }

        public async Task<AuctionsFee?> UpdateAsync(int id, UpdateAuctionFeeDTO dto)
        {
            var auctionFee = await _context.AuctionsFees.FindAsync(id);
            if (auctionFee == null)
            {
                return null;
            }

            // Cập nhật các trường nếu DTO có giá trị
            auctionFee.Description = dto.Description ?? auctionFee.Description;
            auctionFee.FeePerMinute = dto.FeePerMinute ?? auctionFee.FeePerMinute;
            auctionFee.EntryFee = dto.EntryFee ?? auctionFee.EntryFee;
            auctionFee.Currency = dto.Currency ?? auctionFee.Currency;
            auctionFee.Type = dto.Type ?? auctionFee.Type;
            auctionFee.Status = dto.Status ?? auctionFee.Status;
            auctionFee.UpdatedAt = DateTime.Now;

            _context.AuctionsFees.Update(auctionFee);
            return auctionFee;
        }

        public async Task<int> CountAsync(string? status, string? type)
        {
            var query = _context.AuctionsFees.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.Status == status);
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(x => x.Type == type);
            }

            return await query.CountAsync();
        }

        public async Task<AuctionsFee> GetAuctionsFeeByAuctionIdAsync(int auctionId)
        {
            var auctionFee = await _context.AuctionsFees.FirstOrDefaultAsync(af => af.AuctionsId == auctionId);

            return auctionFee!;
        }
    }
}