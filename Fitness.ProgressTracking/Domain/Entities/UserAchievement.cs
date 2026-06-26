namespace Fitness.ProgressTracking.Domain.Entities;

public class UserAchievement
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AchievementId { get; set; }
    public DateTime EarnedAt { get; set; } = DateTime.UtcNow;

    public Achievement Achievement { get; set; } = null!;
}
