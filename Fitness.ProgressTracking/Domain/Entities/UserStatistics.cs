namespace Fitness.ProgressTracking.Domain.Entities;

public class UserStatistics : BaseEntity
{
    public string UserId { get; set; }
    public int TotalWorkouts { get; set; }
    public int TotalCaloriesBurned { get; set; }
    public double TotalWeightLost { get; set; }
    public double CurrentWeight { get; set; }
    public double StartWeight { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
