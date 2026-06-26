namespace Fitness.Nutrition.Domain.Entities;

public class NutritionFacts
{
    public int Id { get; set; }
    public int Calories { get; set; }
    public double Protein { get; set; }
    public double Carbs { get; set; }
    public double Fats { get; set; }
    public double Fiber { get; set; }

    public Meal Meal { get; set; } = null!;
}
