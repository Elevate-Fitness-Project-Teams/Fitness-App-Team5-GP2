namespace Fitness.ProgressTracking.Domain.Entities;

public class WeightHistory :BaseEntity
{
    public string UserId { get; set; }
    public double Weight { get; set; }
    public DateTime Date { get; set; }
    public string? Notes { get; set; }
}
