using Fitness.CalculationEngine.Shared.Services;
using Fitness.Nutrition.Domain.Entities;
using Fitness.Nutrition.Features.Meals.Common;
using Fitness.Nutrition.Infrastructure.Integrations.FCEService;
using Fitness.Nutrition.Infrastructure.Persistence.Repositories;
using Fitness.Nutrition.Shared.Requests;
using Fitness.Nutrition.Shared.Response;
using LinqKit;
using MediatR;

namespace Fitness.Nutrition.Features.Meals.GetPersonalizedMealRecommendations;

public record GetPersonalizedMealRecommendationsQuery
(string UserId ,string? MealType, int? Page, double? MaxCalories, double? MinProtein)
    : IRequest<RequestResult<GetPersonalizedMealRecommendationsResponse>>;

public class GetPersonalizedMealRecommendationsQueryHandler(FceGrpcClient fceGrpcClient 
    ,Repository<Meal> repository)
    : IRequestHandler<GetPersonalizedMealRecommendationsQuery, RequestResult<GetPersonalizedMealRecommendationsResponse>>
{
    private readonly FceGrpcClient _fceGrpcClient = fceGrpcClient;
    private readonly Repository<Meal> _repository = repository;

    public async Task<RequestResult<GetPersonalizedMealRecommendationsResponse>> Handle(GetPersonalizedMealRecommendationsQuery request, CancellationToken cancellationToken)
    {
        var userMatreics = await _fceGrpcClient.GetUserMetricsAsync(request.UserId, cancellationToken);
        if (!userMatreics.IsSuccess)
            return RequestResult<GetPersonalizedMealRecommendationsResponse>.Failure(ResultCode.FCEMetricsNotCalculated);
        var predicate = PredicateBuilder.New<Meal>(true);
        if (request.MealType is not null)
            predicate = predicate.And(m => m.Type.Contains(request.MealType));
        if(request.MaxCalories is not null)
            predicate = predicate.And(m => m.Calories <= request.MaxCalories);
        if (request.MinProtein is not null)
            predicate = predicate.And(m => m.Protein >= request.MinProtein);
        var query = _repository.Get(predicate).Select(m => new MealSummaryResponse
        (m.Id,m.Name,m.Type,m.Calories,m.Protein,m.Carbs,m.Fats));
        
        var pagedResult = await query.PaginateAsync(new PaginationParams
        {
           Page = request.Page ?? 1
        },cancellationToken);

        var response = new GetPersonalizedMealRecommendationsResponse
        (userMatreics.Data.CalorieTarget, pagedResult);
            
        return RequestResult<GetPersonalizedMealRecommendationsResponse>.succeeded(response, ResultCode.RecommendationsRetrivedSuccesses);
    }
}



