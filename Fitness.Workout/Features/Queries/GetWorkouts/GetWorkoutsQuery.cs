using BuildingBlocks.Models;
using MediatR;

namespace Fitness.Workout.Features.Queries.GetWorkouts;

public class GetWorkoutsQuery : IRequest<ApiResponse<List<WorkoutListDto>>>
{
    public string? Category { get; set; }
    public string? Difficulty { get; set; }
    public string? Search { get; set; }
    public int? Duration { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
