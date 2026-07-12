namespace Fitness.Nutrition.Domain.Entities;

public class MealPlanItem :BaseEntity
{
    public Guid MealPlanId { get; set; }
    public Guid MealId { get; set; }
    public string DayOfWeek { get; set; } = string.Empty;
    public string MealTime { get; set; } = string.Empty;

    public MealPlan MealPlan { get; set; } = null!;
    public Meal Meal { get; set; } = null!;
}
