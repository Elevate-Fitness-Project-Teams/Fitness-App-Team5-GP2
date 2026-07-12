namespace Fitness.Nutrition.Shared.Constants;

public static class Endpoints
{
    private const string Base = "api/v1/nutrition";
    public static class Meals
    {
        private const string BaseMeal = $"{Base}";
        public const string MealRecommendations = $"{BaseMeal}/recommendations";
    }

}
