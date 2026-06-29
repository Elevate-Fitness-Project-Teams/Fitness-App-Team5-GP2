using BuildingBlocks.Models;
using MediatR;

namespace Fitness.Workout.Features.Queries.GetWorkoutById;

public class GetWorkoutByIdQuery : IRequest<ApiResponse<WorkoutDetailDto>>
{
    public int WorkoutId { get; set; }

    public GetWorkoutByIdQuery(int workoutId)
    {
        WorkoutId = workoutId;
    }
}
