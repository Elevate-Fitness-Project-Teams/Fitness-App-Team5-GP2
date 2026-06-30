using Fitness.CalculationEngine.Domain.Enums;

namespace Fitness.CalculationEngine.Domain.Entities;

public class FitnessPlanConfig : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Goal Goal { get; set; } 
    public UserStatus Status { get; set; }
    public double MinCalorie { get; set; }
    public double MaxCalorie { get; set; }
    public int WorkoutsPerWeek { get; set; }
    public string EstimatedDuration { get; set; } = string.Empty;
    public string ProgramType { get; set; } = string.Empty;

    public ICollection<UserAssignedPlan> UserAssignedPlans { get; set; } = [];
}
