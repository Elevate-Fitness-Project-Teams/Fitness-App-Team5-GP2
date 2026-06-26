namespace Fitness.Workout.Domain.Entities;

public class WorkoutSession
{
    public string SessionId { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int WorkoutId { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Active";

    public Workout Workout { get; set; } = null!;
}
