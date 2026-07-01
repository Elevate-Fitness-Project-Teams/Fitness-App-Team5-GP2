using BuildingBlocks.Models;
using Fitness.Workout.Repositories;
using MediatR;

namespace Fitness.Workout.Features.Queries.GetWorkouts;

public class GetWorkoutsQueryHandler(IWorkoutRepository repository)
    : IRequestHandler<GetWorkoutsQuery, ApiResponse<List<WorkoutListDto>>>
{
    public async Task<ApiResponse<List<WorkoutListDto>>> Handle(
        GetWorkoutsQuery request,
        CancellationToken cancellationToken)
    {
        var workouts = await repository.GetAllAsync(
            request.Category,
            request.Difficulty,
            request.Search,
            request.Duration,
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
