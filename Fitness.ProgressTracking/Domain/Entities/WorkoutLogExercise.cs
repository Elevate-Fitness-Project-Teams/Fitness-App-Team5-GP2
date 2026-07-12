namespace Fitness.ProgressTracking.Domain.Entities;

public class WorkoutLogExercise : BaseEntity
{
    public Guid WorkoutLogId { get; set; }
    public int ExerciseId { get; set; }
    public int SetsCompleted { get; set; }
    public int RepsCompleted { get; set; }                 
    public double WeightUsed { get; set; }
    public bool Completed { get; set; }

    public WorkoutLog WorkoutLog { get; set; } = null!;
}
