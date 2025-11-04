using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.AuctionParticipantDTO;
using EV.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;
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

        public async Task Refund(int auctionId)
        {
            var participants = await _unitOfWork.auctionParticipantRepository.GetListUserInAuction(auctionId);
            var auction = await _unitOfWork.auctionRepository.GetAuctionById(auctionId);

            if (auction == null || participants == null || !participants.Any())
                return;

            var highestBid = await _unitOfWork.auctionBidRepository.GetHighestBid(auctionId);                                

            if (highestBid == null)
                return;

            var winnerUserId = highestBid.BidderId;

            foreach (var participant in participants)
            {
                if (participant.UserId == winnerUserId)
                {
                    participant.IsWinningBid = true;
                    participant.RefundStatus = "PendingConfirmation";
                    participant.Status = "Won"; 
                }
                else if (participant.UserId.HasValue && participant.RefundStatus != "Refunded" && participant.Status == "Active")
                {
                    var user = await _unitOfWork.userRepository.GetUserById(participant.UserId.Value);
                    if (user != null)
                    {
                        var refundAmount = participant.DepositAmount * 0.9m;
                        user.Wallet = (user.Wallet ?? 0) + refundAmount;

                        await _unitOfWork.userRepository.UpdateUser(user);

                        participant.RefundStatus = "Refunded";
                        participant.Status = "Lost";
                    }
                }

                _unitOfWork.auctionParticipantRepository.Update(participant);
            }

            await _unitOfWork.SaveChangesAsync();
        }




        public void UpdateAuctionParticipant(AuctionParticipant auctionParticipant)
        {
            _unitOfWork.auctionParticipantRepository.Update(auctionParticipant);
        }
    }
}
