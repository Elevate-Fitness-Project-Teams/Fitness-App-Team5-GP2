namespace Fitness.Nutrition.Domain.Entities;

public class Meal
{
    public int MealId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double Calories { get; set; }
    public double Protein { get; set; }
    public double Carbs { get; set; }
    public double Fats { get; set; }
    public int PrepTimeInMinutes { get; set; }
    public string Difficulty { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string IngredientsJson { get; set; } = string.Empty;
    public string InstructionsJson { get; set; } = string.Empty;
    public string? VariationsJson { get; set; }
    public string AllergensJson { get; set; } = string.Empty;
    public string TagsJson { get; set; } = string.Empty;

    public NutritionFacts NutritionFacts { get; set; } = null!;
    public ICollection<MealIngredient> MealIngredients { get; set; } = [];
    public ICollection<MealPlanItem> MealPlanItems { get; set; } = [];
}
