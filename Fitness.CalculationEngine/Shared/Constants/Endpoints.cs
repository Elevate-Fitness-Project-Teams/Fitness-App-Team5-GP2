namespace Fitness.CalculationEngine.Shared.Constants;

public static class Endpoints
{
    private const string Base = "api/v1";
    public static class UserFitnessStat
    {
        private const string BaseUserFitnessStat = $"{Base}/fitness";
        public const string SubmitWeightGoalActivity = $"{BaseUserFitnessStat}/weight-goal-activity";
        public const string GetFitnessStats = $"{BaseUserFitnessStat}/stats";
    }

    public static class CalculatedMatrics
    {
        private const string BaseCalculatedMatrics = $"{Base}/fitness";
        public const string Calculate = $"{BaseCalculatedMatrics}/calculate";
        public const string GetFitnessMatrics = $"{BaseCalculatedMatrics}/metrics";
    }
    public static class AssignedPlan
    {
        private const string BaseAssignedPlan = $"{Base}/fitness";
        public const string AssignPlan = $"{BaseAssignedPlan}/assign-plan";
    }

    public static class FitnessPlanConfig
    {
        private const string BaseAssignedPlan = $"{Base}/fitness";
        public const string GetFitnessPlanConfig = $"{BaseAssignedPlan}/plan-configs";
        public const string GetSpecificPlanConfig = $"{BaseAssignedPlan}/plans/{{planId}}";
    }
}
