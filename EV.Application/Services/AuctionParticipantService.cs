using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
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

        public async Task CreateAuctionParticipantAsync(AuctionParticipant auctionParticipant)
        {
            await _unitOfWork.auctionParticipantRepository.CreateAsync(auctionParticipant);
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
