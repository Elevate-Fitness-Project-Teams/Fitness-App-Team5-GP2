using BuildingBlocks.Models;
using Fitness.Workout.Features.Queries.GetWorkouts;
using Fitness.Workout.Repositories;
using MediatR;

namespace Fitness.Workout.Features.Queries.GetWorkoutsByCategory;

public class GetWorkoutsByCategoryQueryHandler(IWorkoutRepository repository)
    : IRequestHandler<GetWorkoutsByCategoryQuery, ApiResponse<List<WorkoutListDto>>>
{
    public async Task<ApiResponse<List<WorkoutListDto>>> Handle(
        GetWorkoutsByCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var workouts = await repository.GetByCategoryAsync(
            request.CategoryName,
            request.Page,
            request.PageSize,
            cancellationToken);

        var result = workouts.Select(w => new WorkoutListDto
        {
            WorkoutId = w.WorkoutId,
            Name = w.Name,
            Category = w.Category,
            DurationInMinutes = w.DurationInMinutes,
            Difficulty = w.Difficulty,
            CaloriesBurn = w.CaloriesBurn,
            ImageUrl = w.ImageUrl,
            IsPremium = w.IsPremium
        }).ToList();

        return ApiResponse<List<WorkoutListDto>>.Successed(result);
    }
}
