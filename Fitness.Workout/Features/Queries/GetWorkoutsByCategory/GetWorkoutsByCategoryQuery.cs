using BuildingBlocks.Models;
using Fitness.Workout.Features.Queries.GetWorkouts;
using MediatR;

namespace Fitness.Workout.Features.Queries.GetWorkoutsByCategory;

public class GetWorkoutsByCategoryQuery : IRequest<ApiResponse<List<WorkoutListDto>>>
{
    public string CategoryName { get; set; } = string.Empty;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public GetWorkoutsByCategoryQuery(string categoryName, int page, int pageSize)
    {
        CategoryName = categoryName;
        Page = page;
        PageSize = pageSize;
    }
}
