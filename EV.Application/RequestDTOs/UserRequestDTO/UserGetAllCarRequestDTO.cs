namespace EV.Application.RequestDTOs.UserRequestDTO
{
    public class UserGetAllCarRequestDTO
    {
        public int UserId { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
