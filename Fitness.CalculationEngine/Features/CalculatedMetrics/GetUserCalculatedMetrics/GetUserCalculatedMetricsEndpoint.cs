using BuildingBlocks.Models;
using Fitness.CalculationEngine.Shared.Constants;
using Fitness.CalculationEngine.Shared.Interfaces;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;

namespace Fitness.CalculationEngine.Features.CalculatedMetrics.GetUserCalculatedMetrics;

public class GetUserCalculatedMetricsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{Endpoints.CalculatedMatrics.GetFitnessMatrics}/{{userId}}",
            async (string userId, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetUserCalculatedMetricsQuery(userId));
            return result.Code switch
            {
                ResultCode.UserNotFound => Results.BadRequest(ApiResponse<GetUserCalculatedMetricsResponse>.Failure("FCE_METRICS_NOT_CALCULATED.")),
                ResultCode.UserCalculatedMetricsReturnSucess => Results.Ok(ApiResponse<GetUserCalculatedMetricsResponse>.Successed(result.Result)),
            };
        });
    }
}
