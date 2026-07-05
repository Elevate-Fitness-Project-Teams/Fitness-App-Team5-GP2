using Fitness.ProgressTracking.Shared.Response;
using MediatR;

namespace Fitness.ProgressTracking.Features.LogWorkoutCompletion;

public record LogWorkoutCompletionCommand
(string WorkoutId , string SessionId ,DateTime CompletedAt ,int Duration ,int CaloriesBurned 
    ,string Difficulty , string Notes ,int Rating ,bool ExercisesCompleted)
    : IRequest<RequestResult<LogWorkoutCompletionResponse>>;

public class LogWorkoutCompletionCommandHandler : IRequestHandler<LogWorkoutCompletionCommand, RequestResult<LogWorkoutCompletionResponse>>
{
    public async Task<RequestResult<LogWorkoutCompletionResponse>> Handle(LogWorkoutCompletionCommand request, CancellationToken cancellationToken)
    {
        var logId = Guid.NewGuid();
        var response = new LogWorkoutCompletionResponse(logId);
        return RequestResult<LogWorkoutCompletionResponse>.succeeded(response,ResultCode.WorkoutLoggedSuccessfully);
    }
}
