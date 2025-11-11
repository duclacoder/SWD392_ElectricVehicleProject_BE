using EV.Application.CustomEntities;
using EV.Application.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IDashboardService
    {
        Task<ResponseDTO<DashboardOverviewCustom>> GetDashboardOverviewAsync();
        Task<ResponseDTO<AuctionReportCustom>> GetAuctionReportAsync(DateTime? startDate, DateTime? endDate);
        Task<ResponseDTO<PackageRevenueReportCustom>> GetPackageRevenueReportAsync(DateTime? startDate, DateTime? endDate);
        Task<ResponseDTO<IEnumerable<RevenueByTimeCustom>>> GetRevenueByTimeAsync(DateTime startDate, DateTime endDate);

        Task<ResponseDTO<IEnumerable<PackageStatisticCustom>>> GetPackageStatisticsAsync();

        Task<ResponseDTO<PostStatisticCustom>> GetPostStatisticsAsync();
    }
}

