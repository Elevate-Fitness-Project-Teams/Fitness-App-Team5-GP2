namespace Fitness.Nutrition.Domain.Entities;

public class Ingredient
{
    public int IngredientId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<MealIngredient> MealIngredients { get; set; } = [];
}
