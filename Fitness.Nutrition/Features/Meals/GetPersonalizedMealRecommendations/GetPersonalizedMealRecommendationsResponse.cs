using Fitness.Nutrition.Features.Meals.Common;
using Fitness.Nutrition.Shared.Response;

namespace Fitness.Nutrition.Features.Meals.GetPersonalizedMealRecommendations;

public record GetPersonalizedMealRecommendationsResponse
   (double UserDailyGoalCalories, PaginatedResult<MealSummaryResponse> Meals);
