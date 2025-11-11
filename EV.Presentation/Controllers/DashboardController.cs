using EV.Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EV.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Lấy tổng quan dữ liệu dashboard (tổng user, revenue, v.v.)
        /// </summary>
        [HttpGet("overview")]
        public async Task<IActionResult> GetDashboardOverview()
        {
            var result = await _dashboardService.GetDashboardOverviewAsync();
            return Ok(result);
        }

        /// <summary>
        /// Lấy thống kê các gói (Package Statistics)
        /// </summary>
        [HttpGet("package-statistics")]
        public async Task<IActionResult> GetPackageStatistics()
        {
            var result = await _dashboardService.GetPackageStatisticsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Lấy thống kê bài đăng (Post Statistics)
        /// </summary>
        [HttpGet("post-statistics")]
        public async Task<IActionResult> GetPostStatistics()
        {
            var result = await _dashboardService.GetPostStatisticsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Báo cáo doanh thu theo gói (Package Revenue)
        /// </summary>
        [HttpGet("package-revenue")]
        public async Task<IActionResult> GetPackageRevenueReport([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var result = await _dashboardService.GetPackageRevenueReportAsync(startDate, endDate);
            return Ok(result);
        }

        /// <summary>
        /// Báo cáo đấu giá (Auction Report)
        /// </summary>
        [HttpGet("auction-report")]
        public async Task<IActionResult> GetAuctionReport([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var result = await _dashboardService.GetAuctionReportAsync(startDate, endDate);
            return Ok(result);
        }

        /// <summary>
        /// Doanh thu theo thời gian (Revenue By Time)
        /// </summary>
        [HttpGet("revenue-by-time")]
        public async Task<IActionResult> GetRevenueByTime([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _dashboardService.GetRevenueByTimeAsync(startDate, endDate);
            return Ok(result);
        }
    }
}
