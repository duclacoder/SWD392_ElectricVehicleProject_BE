using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.RequestDTOs.AuctionRequestDTO;
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
    public class AuctionRepository : IAuctionRepository
    {
        private readonly Swd392Se1834G2T1Context _context;

        public AuctionRepository(Swd392Se1834G2T1Context context)
        {
            _context = context;
        }

        public async Task<AuctionCustom> CreateAuction(CreateAuctionDTO createAuctionDTO)
        {
            if (!int.TryParse(createAuctionDTO.UserName, out int userId))
                throw new Exception("Invalid userId format");

            var seller = await _context.Users.FindAsync(userId);

            if (seller == null) throw new Exception("User not found");

            var vehicle = await _context.Vehicles.FindAsync(createAuctionDTO.VehicleId);

            if (vehicle == null) throw new Exception("Vehicle not found");

            createAuctionDTO.UserName = seller.UserName;

            var auction = new Auction
            {
                VehicleId = vehicle.VehiclesId,
                SellerId = seller.UsersId,
                StartPrice = createAuctionDTO.StartPrice,
                StartTime = DateTime.Now,
                EndTime = createAuctionDTO.EndTime,
                AuctionsFeeId = createAuctionDTO.AuctionsFeeId,
                FeePerMinute = createAuctionDTO.FeePerMinute,
                OpenFee = createAuctionDTO.OpenFee,
                EntryFee = createAuctionDTO.EntryFee,
                Status = "Active"
            };
            
            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync(); // Lưu để có auctionId để response về!

            return new AuctionCustom
            {
                AuctionId = auction.AuctionsId,
                SellerUserName = seller.UserName,
                VehicleId = vehicle.VehiclesId,
                StartPrice = createAuctionDTO.StartPrice,
                StartTime = DateTime.Now,
                EndTime = (DateTime)createAuctionDTO.EndTime,
                AuctionsFeeId = createAuctionDTO.AuctionsFeeId,
                FeePerMinute = createAuctionDTO .FeePerMinute,
                OpenFee = createAuctionDTO.OpenFee,
                EntryFee = createAuctionDTO?.EntryFee,
                Status = auction.Status,
            };
        }

        public async Task<AuctionCustom> DeleteAuction(int id)
        {
            var auction = await _context.Auctions.FirstAsync(a => a.AuctionsId == id);
            if (auction == null) return null;

            auction.Status = "InActive";
            
            _context.Auctions.Update(auction);

            return new AuctionCustom
            {
                AuctionId = auction.AuctionsId,
                SellerUserName = auction.Seller.UserName,
                VehicleId = (int)auction.VehicleId,
                StartPrice = (decimal)auction.StartPrice,
                StartTime = (DateTime)auction.StartTime,
                EndTime = (DateTime)auction.EndTime,
                AuctionsFeeId = auction.AuctionsFeeId,
                FeePerMinute = auction.FeePerMinute,
                OpenFee = auction.OpenFee,
                EntryFee = auction.EntryFee,
                Status = auction.Status,
            };
        }

        public async Task<IEnumerable<AuctionCustom>> GetAllAuctions(int skip, int take, string sellerUserName)
        {
            var items = _context.Auctions
                                .Include(a => a.Seller)
                                .Include(a => a.AuctionBids)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(sellerUserName))
            {
                items = items.Where(a => a.Seller.UserName == sellerUserName);
            }

            var auctions = await items.Skip(skip).Take(take).ToListAsync();

            return auctions.Select(a => new AuctionCustom
            {
                AuctionId = a.AuctionsId,
                SellerUserName = a.Seller.UserName,
                VehicleId = (int)a.VehicleId,
                StartPrice = (decimal)a.StartPrice,
                StartTime = (DateTime)a.StartTime,
                EndTime = (DateTime)a.EndTime,
                AuctionsFeeId = a.AuctionsFeeId,
                FeePerMinute = a.FeePerMinute,
                OpenFee = a.OpenFee,
                EntryFee = a.EntryFee,
                Status = a.Status,
                Bids = a.AuctionBids?.Select(b => new AuctionBidCustom
                {
                    AuctionBidId = b.AuctionBidsId,
                    BidderUserName = b.Bidder?.UserName,
                    BidAmount = b.BidAmount,
                    BidTime = b.BidTime,
                }).ToList() ?? new List<AuctionBidCustom>()
            });
        }

        public async Task<AuctionCustom> GetAuctionById(int id)
        {
            var auction = await _context.Auctions
                                        .Include(a => a.Seller)
                                        .Include(a => a.AuctionBids)
                                        .ThenInclude(a => a.Bidder)
                                        .FirstAsync(a => a.AuctionsId == id);

            if (auction == null) return null;

            return new AuctionCustom
            {
                AuctionId = auction.AuctionsId,
                SellerUserName = auction.Seller.UserName,
                VehicleId = (int)auction.VehicleId,
                StartPrice = (decimal)auction.StartPrice,
                StartTime = (DateTime)auction.StartTime,
                EndTime = (DateTime)auction.EndTime,
                AuctionsFeeId = auction.AuctionsFeeId,
                FeePerMinute = auction.FeePerMinute,
                OpenFee = auction.OpenFee,
                EntryFee = auction.EntryFee,
                Status = auction.Status,
                Bids = auction.AuctionBids?.Select(b => new AuctionBidCustom
                {
                    AuctionBidId = b.AuctionBidsId,
                    BidderUserName = b.Bidder.UserName,
                    BidAmount = b.BidAmount ?? 0,
                    BidTime = b.BidTime ?? null
                }).ToList() ?? new List<AuctionBidCustom>()
            };
        }

        public async Task<AuctionCustom> UpdateAuction(int id, UpdateAuctionDTO dto)
        {
            var auction = await _context.Auctions
                .Include(a => a.Seller)
                .FirstOrDefaultAsync(a => a.AuctionsId == id);

            if (auction == null)
                return null;

            if (dto.Status != null)
                auction.Status = dto.Status;

            if (dto.EndTime.HasValue)
                auction.EndTime = dto.EndTime.Value;

            await _context.SaveChangesAsync();

            return new AuctionCustom
            {
                AuctionId = auction.AuctionsId,
                SellerUserName = auction.Seller?.UserName ?? "",
                VehicleId = auction.VehicleId ?? 0,
                StartPrice = auction.StartPrice ?? 0,
                StartTime = auction.StartTime ?? DateTime.MinValue,
                EndTime = auction.EndTime ?? DateTime.MinValue,
                AuctionsFeeId = auction.AuctionsFeeId,
                FeePerMinute = auction.FeePerMinute,
                OpenFee = auction.OpenFee,
                EntryFee = auction.EntryFee,
                Status = auction.Status
            };
        }
    }
}
