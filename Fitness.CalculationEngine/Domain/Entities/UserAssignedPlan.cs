namespace Fitness.CalculationEngine.Domain.Entities;

public class UserAssignedPlan
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string PlanId { get; set; } = string.Empty;
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public FitnessPlanConfig FitnessPlanConfig { get; set; } = null!;
}
