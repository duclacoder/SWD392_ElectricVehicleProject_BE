namespace EV.Presentation.RequestModels.AdminRequests
{
    public class UpdateUserModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public string Status { get; set; }
    }
}