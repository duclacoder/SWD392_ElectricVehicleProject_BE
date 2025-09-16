namespace EV.Domain.Entities;

public partial class User
{
    public int UserId { get; set; }

    public int? RoleId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string Password { get; set; } = null!;

    public string? SocialLoginProvider { get; set; }

    public string? SocialLoginId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Status { get; set; }

    public virtual Role? Role { get; set; }
}
