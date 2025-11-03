using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.AuctionRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuctionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CloseAuction(int auctionId)
        {
            var auction = await _unitOfWork.auctionRepository.GetAuctionById(auctionId);
            if (auction == null)
                return;

            var auctionCustom = new UpdateAuctionDTO()
            {
                Status = "ended",
            };

            await _unitOfWork.auctionRepository.UpdateAuction(auctionId, auctionCustom);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ResponseDTO<AuctionCustom>> CreateAuction(CreateAuctionDTO createAuctionDTO)
        {
            var result = await _unitOfWork.auctionRepository.CreateAuction(createAuctionDTO);
            if(result == null)
            {
                return new ResponseDTO<AuctionCustom>("Seller or Vehicle not found", false);
            }

            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO<AuctionCustom>("Created successfully", true, result);
        }

        public async Task<ResponseDTO<AuctionCustom>> DeleteAuction(int id)
        {
            var result = await _unitOfWork.auctionRepository.DeleteAuction(id);
            if(result == null)
                return new ResponseDTO<AuctionCustom>("Auction not found", false);

            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO<AuctionCustom>("Deleted successfully", true, result);
        }

        public async Task<ResponseDTO<PagedResult<AuctionCustom>>> GetAllAuction(GetAllAuctionRequestDTO getAllAuctionRequestDTO)
        {
            var data = await _unitOfWork.auctionRepository.GetAllAuctions(
                (getAllAuctionRequestDTO.Page - 1) * getAllAuctionRequestDTO.PageSize,
                getAllAuctionRequestDTO.PageSize,
                getAllAuctionRequestDTO.UserName
                );
            
            var totalItems = data.Count();

            var pageResult = new PagedResult<AuctionCustom>
            {
                Items = data.ToList(),
                TotalItems = totalItems,
                Page = getAllAuctionRequestDTO.Page,
                PageSize = getAllAuctionRequestDTO.PageSize,
                TotalPages = (int)Math.Ceiling(totalItems / (double)getAllAuctionRequestDTO.PageSize)
            };

            return new ResponseDTO<PagedResult<AuctionCustom>>("Get successfully", true, pageResult);
        }

        public async Task<ResponseDTO<AuctionCustom>> GetAuctionById(int id)
        {
            var result = await _unitOfWork.auctionRepository.GetAuctionById(id);
            if(result == null)
                return new ResponseDTO<AuctionCustom>("Auction not found", false);

            return new ResponseDTO<AuctionCustom>("Get successfully", true, result);
        }

        public async Task<ResponseDTO<AuctionCustom>> UpdateAuction(int id, UpdateAuctionDTO updateAuctionDTO)
        {
            var result = await _unitOfWork.auctionRepository.UpdateAuction(id, updateAuctionDTO);
            if(result == null)
                return new ResponseDTO<AuctionCustom>("Auction not found", false);

            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO<AuctionCustom>("Updated successfully", true, result);
        }
    }
}
