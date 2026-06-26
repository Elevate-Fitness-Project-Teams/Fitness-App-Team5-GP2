namespace Fitness.CalculationEngine.Domain.Entities;

public class CalculatedMetrics
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public double Bmr { get; set; }
    public double Tdee { get; set; }
    public double CalorieTarget { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
}
