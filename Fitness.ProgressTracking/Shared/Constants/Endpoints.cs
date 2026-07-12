namespace Fitness.ProgressTracking.Shared.Constants;

public static class Endpoints
{
    private const string Base = "api/v1/progress";
    public static class WorkoutLogs
    {
        private const string BaseWorkoutLogs = $"{Base}";
        public const string LogWorkoutCompletion = $"{BaseWorkoutLogs}/workouts";
        public const string GetWorkoutLogs = $"{BaseWorkoutLogs}";
    }
}
