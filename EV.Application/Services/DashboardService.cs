using EV.Application.CustomEntities;
using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO<AuctionReportCustom>> GetAuctionReportAsync(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var data = await _unitOfWork.dashboardRepository.GetAuctionReport(startDate, endDate);
                return new ResponseDTO<AuctionReportCustom>("Success", true, data);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<AuctionReportCustom>($"Error: {ex.Message}", false);
            }
        }

        public async Task<ResponseDTO<DashboardOverviewCustom>> GetDashboardOverviewAsync()
        {
            try
            {
                var data = await _unitOfWork.dashboardRepository.GetDashboardOverview();
                return new ResponseDTO<DashboardOverviewCustom>("Success", true, data);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<DashboardOverviewCustom>($"Error: {ex.Message}", false);
            }
        }

        public async Task<ResponseDTO<PackageRevenueReportCustom>> GetPackageRevenueReportAsync(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var data = await _unitOfWork.dashboardRepository.GetPackageRevenueReport(startDate, endDate);
                return new ResponseDTO<PackageRevenueReportCustom>("Success", true, data);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<PackageRevenueReportCustom>($"Error: {ex.Message}", false);
            }
        }

        public async Task<ResponseDTO<IEnumerable<PackageStatisticCustom>>> GetPackageStatisticsAsync()
        {
            try
            {
                var data = await _unitOfWork.dashboardRepository.GetPackageStatistics();
                return new ResponseDTO<IEnumerable<PackageStatisticCustom>>("Success", true, data);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<PackageStatisticCustom>>($"Error: {ex.Message}", false);
            }
        }

        public async Task<ResponseDTO<PostStatisticCustom>> GetPostStatisticsAsync()
        {
            try
            {
                var data = await _unitOfWork.dashboardRepository.GetPostStatistics();
                return new ResponseDTO<PostStatisticCustom>("Success", true, data);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<PostStatisticCustom>($"Error: {ex.Message}", false);
            }
        }

        public async Task<ResponseDTO<IEnumerable<RevenueByTimeCustom>>> GetRevenueByTimeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var data = await _unitOfWork.dashboardRepository.GetRevenueByTime(startDate, endDate);
                return new ResponseDTO<IEnumerable<RevenueByTimeCustom>>("Success", true, data);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<RevenueByTimeCustom>>($"Error: {ex.Message}", false);
            }
        }
    }
}
