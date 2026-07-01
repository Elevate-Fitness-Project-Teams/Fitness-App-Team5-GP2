namespace Fitness.CalculationEngine.Features.CalculatedMetrics.Calculate;

public record CalculateResponse
(double Bmr, double Tdee, double CalorieTarget, string UserStatus);
