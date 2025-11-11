using EV.Application.CustomEntities;
using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly Swd392Se1834G2T1Context _context;

        public DashboardRepository(Swd392Se1834G2T1Context context)
        {
            _context = context;
        }

        public async Task<AuctionReportCustom> GetAuctionReport(DateTime? startDate, DateTime? endDate)
        {
            var queryAble = _context.Auctions.AsQueryable();
            if (startDate.HasValue)
            {
                queryAble = queryAble.Where(a => a.StartTime >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                queryAble = queryAble.Where(a => a.EndTime <= endDate.Value);
            }

            var auctionDetails = await queryAble
                                                .Select(a => new AuctionReportDetailCustom
                                                {
                                                    AuctionId = a.AuctionsId,
                                                    VehicleId = a.VehicleId ?? 0,
                                                    SellerName = a.Seller.UserName,
                                                    StartPrice = a.StartPrice ?? 0,
                                                    FinalPrice = a.AuctionBids.OrderByDescending(b => b.BidAmount)
                                                                              .FirstOrDefault().BidAmount ?? 0,
                                                    TotalBids = a.AuctionBids.Count(),
                                                    StartTime = a.StartTime ?? DateTime.MinValue,
                                                    EndTime = a.EndTime ?? DateTime.MinValue,
                                                    Status = a.Status,
                                                    WinnerName = a.AuctionParticipants.Where(p => p.IsWinningBid == true)
                                                                                      .Select(a => a.User.UserName)
                                                                                      .FirstOrDefault(),
                                                    Revenue = (a.OpenFee ?? 0) + (a.EntryFee ?? 0)
                                                })
                                                .ToListAsync();

            var totalAuctions = auctionDetails.Count;
            var successfulAuctions = auctionDetails.Count(a => a.FinalPrice > 0);
            var failedAuctions = totalAuctions - successfulAuctions;
            var totalRevenue = auctionDetails.Sum(a => a.Revenue);

            return new AuctionReportCustom
            {
                ReportDate = DateTime.UtcNow,
                StartDate = startDate,
                EndDate = endDate,
                TotalAuctions = totalAuctions,
                SuccessfulAuctions = successfulAuctions,
                FailedAuctions = failedAuctions,
                TotalRevenue = totalRevenue,
                AuctionDetails = auctionDetails
            };
        }

        public async Task<DashboardOverviewCustom> GetDashboardOverview()
        {
            var totalUsers = await _context.Users.Where(u => u.RoleId != 3).CountAsync();
            var totalActiveAuctions = await _context.Auctions.Where(a => a.Status == "Active").CountAsync();
            var totalPosts = await _context.UserPosts.Where(a => a.Status == "Active").CountAsync();

            var totalPackageRevenue = await _context.UserPackages
                  .Where(up => up.Status == "Used")
                  .SumAsync(up => up.PurchasedAtPrice ?? 0);

            var totalAuctionRevenue = await _context.Payments
                 .Where(p => p.ReferenceType == "AuctionFee")
                 .SumAsync(p => p.TransferAmount ?? 0);

            return new DashboardOverviewCustom
            {
                TotalUsers = totalUsers,
                TotalActiveAuctions = totalActiveAuctions,
                TotalActivePosts = totalPosts,
                PackageRevenue = totalPackageRevenue,
                AuctionRevenue = totalAuctionRevenue,
                TotalRevenue = totalPackageRevenue + totalAuctionRevenue
            };
        }

        public async Task<PackageRevenueReportCustom> GetPackageRevenueReport(DateTime? startDate, DateTime? endDate)
        {
            var queryAble = _context.UserPackages.Where(a => a.Status == "Used");

            if (startDate.HasValue)
            {
                queryAble = queryAble.Where(a => a.PurchasedAt >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                queryAble = queryAble.Where(a => a.PurchasedAt <= endDate.Value);
            }

            var packageDetails = await queryAble
                .GroupBy(up => new
                {
                    up.PackageId,
                    up.Package.PackageName,
                    up.Package.PostPrice
                })
                .Select(g => new PackageRevenueDetailCustom
                {
                    PackageId = g.Key.PackageId ?? 0,
                    PackageName = g.Key.PackageName ?? "N/A",
                    PricePerPackage = g.Key.PostPrice ?? 0,
                    TotalSold = g.Count(),
                    TotalRevenue = g.Sum(up => up.PurchasedAtPrice ?? 0)
                })
                .ToListAsync();

            var totalPackagesSold = packageDetails.Sum(p => p.TotalSold);
            var totalRevenue = packageDetails.Sum(p => p.TotalRevenue);

            return new PackageRevenueReportCustom
            {
                ReportDate = DateTime.UtcNow,
                StartDate = startDate,
                EndDate = endDate,
                TotalRevenue = totalRevenue,
                TotalPackagesSold = totalPackagesSold,
                PackageDetails = packageDetails
            };
        }


        //sai!
        public async Task<IEnumerable<PackageStatisticCustom>> GetPackageStatistics()
        {
            var packageStats = await _context.PostPackages
                                             .GroupJoin(_context.UserPackages.Where(a => a.Status == "Used"),
                                                        pp => pp.PostPackagesId,
                                                        aa => aa.PackageId,
                                                        (pp, userPackages) => new PackageStatisticCustom
                                                        {
                                                            PackageId = pp.PostPackagesId,
                                                            PackageName = pp.PackageName,
                                                            PostPrice = (decimal)pp.PostPrice,
                                                            TotalSold = userPackages.Count(),
                                                            TotalRevenue = userPackages.Sum(up => up.PurchasedAtPrice ?? 0),
                                                            Status = pp.Status ?? string.Empty
                                                        })
                                             .ToListAsync();
            return packageStats;
        }


        public async Task<PostStatisticCustom> GetPostStatistics()
        {
            var totalPosts = await _context.UserPosts.CountAsync();
            var activePosts = await _context.UserPosts.Where(a => a.Status == "Active").CountAsync();
            var inactivePosts = await _context.UserPosts.Where(a => a.Status == "InActive").CountAsync();
            var vehiclePosts = await _context.UserPosts.Where(p => p.VehicleId != null).CountAsync();
            var batteryPosts = await _context.UserPosts.Where(p => p.BatteryId != null).CountAsync();

            return new PostStatisticCustom
            {
                TotalPosts = totalPosts,
                ActivePosts = activePosts,
                InactivePosts = inactivePosts,
                VehiclePosts = vehiclePosts,
                BatteryPosts = batteryPosts
            };
        }

        public async Task<IEnumerable<RevenueByTimeCustom>> GetRevenueByTime(DateTime startDate, DateTime endDate)
        {
            var revenueData = await _context.Payments
                                            .Where(p => p.TransactionDate >= startDate
                                                         && p.TransactionDate <= endDate
                                                         && p.Status == "Paid")
                                            .GroupBy(p => new
                                            {
                                                Year = p.TransactionDate.Value.Year,
                                                Month = p.TransactionDate.Value.Month
                                            })
                                            .Select(g => new RevenueByTimeCustom
                                            {
                                                Year = g.Key.Year,
                                                Month = g.Key.Month,
                                                PackageRevenue = g.Where(p => p.ReferenceType == "UserPackage")
                                                                  .Sum(p => p.TransferAmount ?? 0),
                                                AuctionRevenue = g.Where(p => p.ReferenceType == "AuctionFee")
                                                                  .Sum(p => p.TransferAmount ?? 0),
                                                TotalRevenue = g.Sum(p => p.TransferAmount ?? 0)
                                            })
                                            .OrderBy(x => x.Year)
                                            .ThenBy(x => x.Month)
                                            .ToListAsync();
            return revenueData;
        }
    }
}