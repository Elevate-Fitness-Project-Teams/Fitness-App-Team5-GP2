namespace Fitness.Workout.Features.Queries.GetWorkoutById;

public class WorkoutExerciseDto
{
    public int OrderIndex { get; set; }
    public int SetsDefault { get; set; }
    public int RepsDefault { get; set; }
    public int RestTimeInSeconds { get; set; }
    public int ExerciseId { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public string TargetMuscles { get; set; } = string.Empty;
    public string Equipment { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public string? VideoUrl { get; set; }
}
