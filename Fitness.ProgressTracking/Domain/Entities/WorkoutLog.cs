namespace Fitness.ProgressTracking.Domain.Entities;

public class WorkoutLog : BaseEntity
{
    public string UserId { get; set; }
    public string WorkoutId { get; set; }
    public string SessionId { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
    public int CaloriesBurned { get; set; }
    public int Rating { get; set; }
    public string? Notes { get; set; }
    public DateTime CompletedAt { get; set; }

    public ICollection<WorkoutLogExercise> WorkoutLogExercises { get; set; } = [];
}
