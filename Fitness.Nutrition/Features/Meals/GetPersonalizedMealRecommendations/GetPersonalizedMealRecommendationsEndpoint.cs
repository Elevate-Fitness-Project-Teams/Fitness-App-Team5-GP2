using BuildingBlocks.Models;
using Fitness.Nutrition.Shared.Constants;
using Fitness.Nutrition.Shared.Interfaces;
using Fitness.Nutrition.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Nutrition.Features.Meals.GetPersonalizedMealRecommendations;

public class GetPersonalizedMealRecommendationsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{Endpoints.Meals.MealRecommendations}",
            async ([AsParameters] GetPersonalizedMealRecommendationsQuery request, IMediator mediator) =>
            {
                var result = await mediator.Send(request);
                return result.Code switch
                {
                    ResultCode.FCEMetricsNotCalculated => Results.BadRequest(ApiResponse<GetPersonalizedMealRecommendationsResponse>.Failure("FCE_METRICS_NOT_CALCULATED.")),
                   _ => Results.Ok(ApiResponse<GetPersonalizedMealRecommendationsResponse>.Successed(result.Result)),
                };
            }
        );
    }
}
