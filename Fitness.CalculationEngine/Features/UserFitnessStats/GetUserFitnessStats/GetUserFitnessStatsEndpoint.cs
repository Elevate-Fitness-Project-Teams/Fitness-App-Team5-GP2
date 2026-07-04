using BuildingBlocks.Models;
using Fitness.CalculationEngine.Shared.Constants;
using Fitness.CalculationEngine.Shared.Interfaces;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;

namespace Fitness.CalculationEngine.Features.UserFitnessStats.GetUserFitnessStats;

public class GetUserFitnessStatsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{Endpoints.UserFitnessStat.GetFitnessStats}/{{userId}}",
            async (string userId, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetUserFitnessStatsQuery(userId));
                return result.Code switch
                {
                    ResultCode.UserNotFound => Results.NotFound(ApiResponse<GetUserFitnessStatsResponse>.Failure("FCE Stats Not Found.")),
                    ResultCode.UserFitnessStatsReturnSucess => Results.Ok(ApiResponse<GetUserFitnessStatsResponse>.Successed(result.Result)),
                };
            });
    }   
}
