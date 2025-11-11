using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EV.Application.CustomEntities;
namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IDashboardRepository
    {
        // Tổng quan dashboard
        Task<DashboardOverviewCustom> GetDashboardOverview();

        // Thống kê Package
        Task<IEnumerable<PackageStatisticCustom>> GetPackageStatistics();

        // Thống kê Post
        Task<PostStatisticCustom> GetPostStatistics();

        Task<IEnumerable<RevenueByTimeCustom>> GetRevenueByTime(DateTime startDate, DateTime endDate);

        Task<PackageRevenueReportCustom> GetPackageRevenueReport(DateTime? startDate, DateTime? endDate);
        Task<AuctionReportCustom> GetAuctionReport(DateTime? startDate, DateTime? endDate);
    }
}
