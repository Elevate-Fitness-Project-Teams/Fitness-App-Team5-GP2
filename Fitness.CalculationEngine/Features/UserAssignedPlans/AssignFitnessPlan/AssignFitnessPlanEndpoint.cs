using BuildingBlocks.Models;
using Fitness.CalculationEngine.Shared.Constants;
using Fitness.CalculationEngine.Shared.Interfaces;
using Fitness.CalculationEngine.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.CalculationEngine.Features.UserAssignedPlans.AssignFitnessPlan;

public class AssignFitnessPlanEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Endpoints.AssignedPlan.AssignPlan, async ([FromBody]AssignFitnessPlanOrchestrator command,
            IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return result.Code switch
            {
                ResultCode.NotMatchedPlanFound => Results.Conflict(ApiResponse<AssignFitnessPlanResponse>.Failure(result.Code.ToString())),
                ResultCode.UserFitnessStatsNotFound => Results.BadRequest(ApiResponse<AssignFitnessPlanResponse>.Failure(result.Code.ToString())),
                ResultCode.UserHasNotGoal => Results.NotFound(ApiResponse<AssignFitnessPlanResponse>.Failure(result.Code.ToString())),
                ResultCode.PlanAssignmentFailed => Results.BadRequest(ApiResponse<AssignFitnessPlanResponse>.Failure(result.Code.ToString())),
                ResultCode.PlanAssignedSuccessfully => Results.Ok(ApiResponse<AssignFitnessPlanResponse>.Successed(result.Result, System.Net.HttpStatusCode.OK)),
                _ => Results.Problem("An unexpected error occurred.")
            };

        })
        .WithName("AssignFitnessPlan")
        .WithTags("UserAssignedPlans");
    }
}
