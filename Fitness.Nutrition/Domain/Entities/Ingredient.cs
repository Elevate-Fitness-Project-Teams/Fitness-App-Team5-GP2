namespace Fitness.Nutrition.Domain.Entities;

public class Ingredient :BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<MealIngredient> MealIngredients { get; set; } = [];
}
