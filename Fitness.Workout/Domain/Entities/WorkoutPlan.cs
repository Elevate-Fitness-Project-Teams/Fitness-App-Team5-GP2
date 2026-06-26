namespace Fitness.Workout.Domain.Entities;

public class WorkoutPlan
{
    public string PlanId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Goal { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;

    public ICollection<Workout> Workouts { get; set; } = [];
}
