using Fitness.CalculationEngine.Domain.Enums;

namespace Fitness.CalculationEngine.Features.CalculatedMetrics.GetUserCalculatedMetrics;

public record GetUserCalculatedMetricsResponse
(double Bmr , double Tdee, double CalorieTarget , UserStatus Status , DateTime CalculatedAt);
