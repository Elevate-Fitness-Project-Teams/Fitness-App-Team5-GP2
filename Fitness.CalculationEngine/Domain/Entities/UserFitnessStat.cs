using Fitness.CalculationEngine.Domain.Enums;

namespace Fitness.CalculationEngine.Domain.Entities;

public class UserFitnessStat :BaseEntity
{
    public string UserId { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public Goal Goal { get; set; } 
    public ActivityLevel ActivityLevel { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
}
