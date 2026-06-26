namespace Fitness.UserProfile.Domain.Entities;

public class Profile
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
    public bool IsPremiumCached { get; set; }
    public DateTime MemberSince { get; set; } = DateTime.UtcNow;

    public UserSettings UserSettings { get; set; } = null!;
    public NotificationSettings NotificationSettings { get; set; } = null!;
    public PrivacySettings PrivacySettings { get; set; } = null!;
}
