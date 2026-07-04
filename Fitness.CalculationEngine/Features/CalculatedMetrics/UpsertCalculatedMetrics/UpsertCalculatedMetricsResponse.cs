namespace Fitness.CalculationEngine.Features.CalculatedMetrics.Calculate;

public record UpsertCalculatedMetricsResponse
(double Bmr, double Tdee, double CalorieTarget, string UserStatus);
