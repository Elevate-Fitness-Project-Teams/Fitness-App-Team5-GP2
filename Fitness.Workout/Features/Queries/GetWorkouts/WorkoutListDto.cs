namespace Fitness.Workout.Features.Queries.GetWorkouts;

public class WorkoutListDto
{
    public int WorkoutId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
    public string Difficulty { get; set; } = string.Empty;
    public int CaloriesBurn { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsPremium { get; set; }
}
