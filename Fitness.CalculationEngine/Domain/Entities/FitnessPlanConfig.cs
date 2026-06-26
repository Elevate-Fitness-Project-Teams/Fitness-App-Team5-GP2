namespace Fitness.CalculationEngine.Domain.Entities;

public class FitnessPlanConfig
{
    public string PlanId { get; set; } = string.Empty;
    public string PlanName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Goal { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public double MinCalorie { get; set; }
    public double MaxCalorie { get; set; }
    public int WorkoutsPerWeek { get; set; }
    public string EstimatedDuration { get; set; } = string.Empty;
    public string ProgramType { get; set; } = string.Empty;

    public ICollection<UserAssignedPlan> UserAssignedPlans { get; set; } = [];
}
