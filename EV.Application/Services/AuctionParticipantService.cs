using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.AuctionParticipantDTO;
using EV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Services
{
    public class AuctionParticipantService : IAuctionParticipantService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuctionParticipantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CheckEligibility(CheckEligibilityRequestDTO request)
        {
            return await _unitOfWork.auctionParticipantRepository.CheckEligibility(request);
        }

        public async Task CreateAuctionParticipantAsync(CreateAuctionParticipantRequestDTO auctionParticipant)
        {
            var newAuctionParticipant = new AuctionParticipant
            {
                PaymentsId = auctionParticipant.PaymentsId,
                UserId = auctionParticipant.UserId,
                AuctionsId = auctionParticipant.AuctionsId,
                DepositAmount = auctionParticipant.DepositAmount,
                DepositTime = auctionParticipant.DepositTime,
                RefundStatus = auctionParticipant.RefundStatus,
                Status = auctionParticipant.Status,
                IsWinningBid = auctionParticipant.IsWinningBid
            };
            await _unitOfWork.auctionParticipantRepository.CreateAsync(newAuctionParticipant);
        }

        public async Task DeleteAuctionParticipant(int auctionParticipantId)
        {
            var item = await _unitOfWork.auctionParticipantRepository.GetByIdAsync(auctionParticipantId);
            _unitOfWork.auctionParticipantRepository.Remove(item);
        }

        public async Task<List<AuctionParticipant>> GetAllAuctionsParticipantAsync()
        {
            return await _unitOfWork.auctionParticipantRepository.GetAllAsync();
        }

        public Task<AuctionParticipant> GetAuctionParticipantByIdAsync(int auctionParticipantId)
        {
            return _unitOfWork.auctionParticipantRepository.GetByIdAsync(auctionParticipantId);
        }

        public Task<bool> IsUserParticipatingInAuction(int userId, int auctionId)
        {
            throw new NotImplementedException();
        }

        public void UpdateAuctionParticipant(AuctionParticipant auctionParticipant)
        {
            _unitOfWork.auctionParticipantRepository.Update(auctionParticipant);
        }
    }
}
