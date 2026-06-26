namespace Fitness.UserProfile.Domain.Entities;

public class PrivacySettings
{
    public int Id { get; set; }
    public string ProfileVisibility { get; set; } = "private";
    public bool ShowProgressToFriends { get; set; }
    public bool AllowDataSharing { get; set; }
}
