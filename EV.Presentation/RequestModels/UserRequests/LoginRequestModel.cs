using System.ComponentModel.DataAnnotations;

namespace EV.Presentation.RequestModels.UserRequests
{
    public class LoginRequestModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
