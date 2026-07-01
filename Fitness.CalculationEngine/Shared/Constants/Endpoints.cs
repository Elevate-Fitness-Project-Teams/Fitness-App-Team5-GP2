namespace Fitness.CalculationEngine.Shared.Constants;

public static class Endpoints
{
    private const string Base = "api/v1";
    public static class UserFitnessStat
    {
        private const string BaseUserFitnessStat = $"{Base}/fitness";
        public const string SubmitWeightGoalActivity = $"{BaseUserFitnessStat}/weight-goal-activity";
    }

    public static class CalculatedMatrics
    {
        private const string BaseCalculatedMatrics = $"{Base}/fitness";
        public const string Calculate = $"{BaseCalculatedMatrics}/calculate";
    }
}
