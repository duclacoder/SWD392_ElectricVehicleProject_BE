using EV.Application.RequestDTOs.AuctionFeeRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.Entities;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IAuctionsFeeService
    {
        Task<ResponseDTO<PagedResult<AuctionsFee>>> GetAllAuctionFees(GetAllAuctionFeeRequestDTO requestDTO);
        Task<ResponseDTO<AuctionsFee>> GetAuctionFeeById(int id);
        Task<ResponseDTO<AuctionsFee>> CreateAuctionFee(CreateAuctionFeeDTO createDTO);
        Task<ResponseDTO<AuctionsFee>> UpdateAuctionFee(int id, UpdateAuctionFeeDTO updateDTO);
        Task<ResponseDTO<AuctionsFee>> DeleteAuctionFee(int id);
        Task<ResponseDTO<AuctionsFee>> UndeleteAuctionFee(int id);
    }
}