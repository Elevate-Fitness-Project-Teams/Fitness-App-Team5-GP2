namespace Fitness.Nutrition.Features.Meals.Common;

public record MealSummaryResponse(Guid Id, string Name, string Type, double Calories
    , double Protein, double Carbs, double Fats);

