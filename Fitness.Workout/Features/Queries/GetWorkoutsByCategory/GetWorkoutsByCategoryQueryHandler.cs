using BuildingBlocks.Models;
using Fitness.Workout.Data;
using Fitness.Workout.Features.Queries.GetWorkouts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Workout.Features.Queries.GetWorkoutsByCategory;

public class GetWorkoutsByCategoryQueryHandler(WorkoutDbContext context)
    : IRequestHandler<GetWorkoutsByCategoryQuery, ApiResponse<List<WorkoutListDto>>>
{
    public async Task<ApiResponse<List<WorkoutListDto>>> Handle(
        GetWorkoutsByCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var workouts = await context.Workouts
            .Where(w => w.Category == request.CategoryName)
            .OrderBy(w => w.WorkoutId)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

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
