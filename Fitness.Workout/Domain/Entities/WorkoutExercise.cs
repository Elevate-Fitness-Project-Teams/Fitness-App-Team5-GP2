namespace Fitness.Workout.Domain.Entities;

public class WorkoutExercise
{
    public int Id { get; set; }
    public int WorkoutId { get; set; }
    public int ExerciseId { get; set; }
    public int OrderIndex { get; set; }
    public int SetsDefault { get; set; }
    public int RepsDefault { get; set; }
    public int RestTimeInSeconds { get; set; }

    public Workout Workout { get; set; } = null!;
    public Exercise Exercise { get; set; } = null!;
}
