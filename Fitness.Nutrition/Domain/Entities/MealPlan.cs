namespace Fitness.Nutrition.Domain.Entities;

public class MealPlan :BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double TargetCalorieRangeMin { get; set; }
    public double TargetCalorieRangeMax { get; set; }

    public ICollection<MealPlanItem> MealPlanItems { get; set; } = [];
}
