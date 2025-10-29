namespace EV.Application.RequestDTOs.AuctionFeeRequestDTO
{
    public class GetAllAuctionFeeRequestDTO
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Status { get; set; }
        public string? Type { get; set; }
    }
}