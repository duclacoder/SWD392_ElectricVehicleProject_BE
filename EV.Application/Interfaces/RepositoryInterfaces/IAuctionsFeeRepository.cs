using EV.Application.RequestDTOs.AuctionFeeRequestDTO;
using EV.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IAuctionsFeeRepository
    {
        Task<IEnumerable<AuctionsFee>> GetAllAsync(int skip, int take, string? status, string? type);
        Task<AuctionsFee?> GetByIdAsync(int id);
        Task<AuctionsFee> CreateAsync(CreateAuctionFeeDTO dto);
        Task<AuctionsFee?> UpdateAsync(int id, UpdateAuctionFeeDTO dto);
        Task<AuctionsFee?> DeleteAsync(int id);
        Task<AuctionsFee?> UndeleteAsync(int id);
        Task<int> CountAsync(string? status, string? type);
    }
}