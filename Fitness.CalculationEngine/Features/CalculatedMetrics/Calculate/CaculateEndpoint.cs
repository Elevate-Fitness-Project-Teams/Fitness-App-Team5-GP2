using BuildingBlocks.Models;
using Fitness.CalculationEngine.Features.UserFitnessStats.WeightGoalActivity;
using Fitness.CalculationEngine.Shared.Constants;
using Fitness.CalculationEngine.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fitness.CalculationEngine.Features.CalculatedMetrics.Calculate;

public class CaculateEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Endpoints.CalculatedMatrics.Calculate,
            async ([FromBody] CalculateCommand request, ICurrentUser currentUser, IMediator _mediator
            , CancellationToken cancellationToken) =>
            {
                var result = await _mediator.Send(request, cancellationToken);
                if (!result.Success)
                    return ApiResponse<CalculateResponse>.Failure(result.Code.ToString(), HttpStatusCode.Conflict);
                return ApiResponse<CalculateResponse>.Successed(result.Result, HttpStatusCode.OK);
            })
        //.RequireAuthorization()
        .WithName("Calculate")
        .WithTags("Fitness Calculation Engine")
        .Produces<ApiResponse<CalculateResponse>>(StatusCodes.Status200OK)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized);
    }
}
