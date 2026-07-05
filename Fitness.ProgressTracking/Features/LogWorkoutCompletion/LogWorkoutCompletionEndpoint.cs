using BuildingBlocks.Models;
using Fitness.ProgressTracking.Shared.Constants;
using Fitness.ProgressTracking.Shared.Interfaces;
using Fitness.ProgressTracking.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.ProgressTracking.Features.LogWorkoutCompletion;

public class LogWorkoutCompletionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Endpoints.WorkoutLogs.LogWorkoutCompletion, 
            async ([FromBody]LogWorkoutCompletionCommand command,IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return result.Code switch
            {
                ResultCode.WorkoutLoggedSuccessfully => Results.Ok(ApiResponse<LogWorkoutCompletionResponse>.Successed(result.Result)),
                ResultCode.FailedToLogWorkout => Results.BadRequest(ApiResponse<LogWorkoutCompletionResponse>.Failure(result.Code.ToString())),
                //ResultCode.NotFound => Results.NotFound(result),
                _ => Results.BadRequest(ApiResponse<LogWorkoutCompletionResponse>.Failure("Failed to log workout completion")),
            };
        });
    }
}
