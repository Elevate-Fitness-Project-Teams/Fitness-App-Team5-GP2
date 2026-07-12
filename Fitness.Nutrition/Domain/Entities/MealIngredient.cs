namespace Fitness.Nutrition.Domain.Entities;

public class MealIngredient :BaseEntity
{
    public Guid MealId { get; set; }
    public Guid IngredientId { get; set; }
    public string Amount { get; set; } = string.Empty;

    public Meal Meal { get; set; } = null!;
    public Ingredient Ingredient { get; set; } = null!;
}
