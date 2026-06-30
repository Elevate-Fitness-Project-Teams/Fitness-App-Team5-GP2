namespace Fitness.CalculationEngine.Domain.Entities;

public class UserAssignedPlan : BaseEntity
{
    public string UserId { get; set; }
    public Guid PlanId { get; set; } 
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public FitnessPlanConfig FitnessPlanConfig { get; set; } = null!;
}
