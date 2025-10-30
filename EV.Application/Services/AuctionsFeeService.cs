using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.AuctionFeeRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EV.Application.Services
{
    public class AuctionsFeeService : IAuctionsFeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuctionsFeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO<AuctionsFee>> CreateAuctionFee(CreateAuctionFeeDTO createDTO)
        {
            var result = await _unitOfWork.auctionsFeeRepository.CreateAsync(createDTO);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO<AuctionsFee>("Created successfully", true, result);
        }

        public async Task<ResponseDTO<AuctionsFee>> DeleteAuctionFee(int id)
        {
            var result = await _unitOfWork.auctionsFeeRepository.DeleteAsync(id);
            if (result == null)
            {
                return new ResponseDTO<AuctionsFee>("AuctionFee not found", false);
            }

            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO<AuctionsFee>("Deleted successfully (Status set to InActive)", true, result);
        }

        public async Task<ResponseDTO<AuctionsFee>> UndeleteAuctionFee(int id)
        {
            var result = await _unitOfWork.auctionsFeeRepository.UndeleteAsync(id);
            if (result == null)
            {
                return new ResponseDTO<AuctionsFee>("AuctionFee not found", false);
            }

            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO<AuctionsFee>("Undeleted successfully (Status set to Active)", true, result);
        }

        public async Task<ResponseDTO<PagedResult<AuctionsFee>>> GetAllAuctionFees(GetAllAuctionFeeRequestDTO requestDTO)
        {
            var data = await _unitOfWork.auctionsFeeRepository.GetAllAsync(
                (requestDTO.Page - 1) * requestDTO.PageSize,
                requestDTO.PageSize,
                requestDTO.Status,
                requestDTO.Type
            );

            var totalItems = await _unitOfWork.auctionsFeeRepository.CountAsync(requestDTO.Status, requestDTO.Type);

            var pageResult = new PagedResult<AuctionsFee>
            {
                Items = data.ToList(),
                TotalItems = totalItems,
                Page = requestDTO.Page,
                PageSize = requestDTO.PageSize,
                TotalPages = (int)Math.Ceiling(totalItems / (double)requestDTO.PageSize)
            };

            return new ResponseDTO<PagedResult<AuctionsFee>>("Get successfully", true, pageResult);
        }

        public async Task<ResponseDTO<AuctionsFee>> GetAuctionFeeById(int id)
        {
            var result = await _unitOfWork.auctionsFeeRepository.GetByIdAsync(id);
            if (result == null)
            {
                return new ResponseDTO<AuctionsFee>("AuctionFee not found", false);
            }

            return new ResponseDTO<AuctionsFee>("Get successfully", true, result);
        }

        public async Task<ResponseDTO<AuctionsFee>> UpdateAuctionFee(int id, UpdateAuctionFeeDTO updateDTO)
        {
            var result = await _unitOfWork.auctionsFeeRepository.UpdateAsync(id, updateDTO);
            if (result == null)
            {
                return new ResponseDTO<AuctionsFee>("AuctionFee not found", false);
            }

            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO<AuctionsFee>("Updated successfully", true, result);
        }

        public async Task<ResponseDTO<AuctionsFee>> GetAuctionsFeeByAuctionIdAsync(int auctionId)
        {
            var result = await _unitOfWork.auctionsFeeRepository.GetAuctionsFeeByAuctionIdAsync(auctionId);
            if(result == null)
            {
                return new ResponseDTO<AuctionsFee>("Auction not found", false);
            }

            return new ResponseDTO<AuctionsFee>("Get successfully", true, result);
        }
    }
}