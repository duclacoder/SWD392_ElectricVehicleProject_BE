using System.ComponentModel.DataAnnotations;

namespace EV.Presentation.RequestModels.UserRequests
{
    public class ProfileUpdateRequestModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        //public string? ImageUrl { get; set; }
    }
}
