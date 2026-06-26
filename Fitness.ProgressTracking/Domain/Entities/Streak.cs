namespace Fitness.ProgressTracking.Domain.Entities;

public class Streak
{
    public int UserId { get; set; }
    public int CurrentStreak { get; set; }
    public int LongestStreak { get; set; }
    public DateTime? LastWorkoutDate { get; set; }
}
