using Fitness.CalculationEngine.Domain.Enums;

namespace Fitness.CalculationEngine.Domain.Entities;

public class CalculatedMetrics : BaseEntity
{
    public string UserId { get; set; }
    public double Bmr { get; set; }
    public double Tdee { get; set; }
    public double CalorieTarget { get; set; }
    public UserStatus Status { get; set; } 
    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
}
