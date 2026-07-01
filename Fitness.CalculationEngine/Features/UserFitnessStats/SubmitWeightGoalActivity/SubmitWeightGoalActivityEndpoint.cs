using BuildingBlocks.Models;
using Fitness.CalculationEngine.Features.UserFitnessStats.WeightGoalActivity;
using Fitness.CalculationEngine.Shared.Constants;
using Fitness.CalculationEngine.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fitness.CalculationEngine.Features.UserFitnessStats.SubmitWeightGoalActivity;

public class SubmitWeightGoalActivityEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Endpoints.UserFitnessStat.SubmitWeightGoalActivity, 
            async ([FromBody] SubmitWeightGoalActivityCommand request ,ICurrentUser currentUser ,IMediator _mediator
            , CancellationToken cancellationToken) =>
        {
            var result = await _mediator.Send(request,cancellationToken);
            if (!result.Success)
                return ApiResponse<SubmitWeightGoalActivityResponse>.Failure(result.Code.ToString(), HttpStatusCode.Conflict);
            return ApiResponse<SubmitWeightGoalActivityResponse>.Successed(result.Result, HttpStatusCode.OK);
        })
        //.RequireAuthorization()
        .WithName("SubmitWeightGoalActivity")
        .WithTags("Fitness Calculation Engine")
        .Produces<ApiResponse<SubmitWeightGoalActivityResponse>>(StatusCodes.Status200OK)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized);
    }
}
