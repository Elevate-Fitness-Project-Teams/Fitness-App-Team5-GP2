namespace Fitness.Workout.Domain.Entities;

public class Workout
{
    public int WorkoutId { get; set; }
    public string PlanId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
    public string Difficulty { get; set; } = string.Empty;
    public int CaloriesBurn { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsPremium { get; set; }

    public WorkoutPlan WorkoutPlan { get; set; } = null!;
    public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = [];
    public ICollection<WorkoutSession> WorkoutSessions { get; set; } = [];
}
