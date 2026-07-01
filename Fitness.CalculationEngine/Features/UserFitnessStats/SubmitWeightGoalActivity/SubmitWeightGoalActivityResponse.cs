namespace Fitness.CalculationEngine.Features.UserFitnessStats.WeightGoalActivity;

public record SubmitWeightGoalActivityResponse
(Guid StatsId, string UserId, DateTimeOffset RecordedAt);
