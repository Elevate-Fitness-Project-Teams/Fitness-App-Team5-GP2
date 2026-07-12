namespace Fitness.ProgressTracking.Domain.Entities;

public class UserAchievement : BaseEntity
{
    public string UserId { get; set; }
    public Guid AchievementId { get; set; }
    public DateTime EarnedAt { get; set; } = DateTime.UtcNow;

    public Achievement Achievement { get; set; } = null!;
}
