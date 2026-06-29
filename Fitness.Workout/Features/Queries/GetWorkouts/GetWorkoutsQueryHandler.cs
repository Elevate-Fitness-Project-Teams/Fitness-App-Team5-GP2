using BuildingBlocks.Models;
using Fitness.Workout.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Workout.Features.Queries.GetWorkouts;

public class GetWorkoutsQueryHandler(WorkoutDbContext context)
    : IRequestHandler<GetWorkoutsQuery, ApiResponse<List<WorkoutListDto>>>
{
    public async Task<ApiResponse<List<WorkoutListDto>>> Handle(
        GetWorkoutsQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Workouts.AsQueryable();

        if (!string.IsNullOrEmpty(request.Category))
            query = query.Where(w => w.Category == request.Category);

        if (!string.IsNullOrEmpty(request.Difficulty))
            query = query.Where(w => w.Difficulty == request.Difficulty);

        if (request.Duration is not null)
            query = query.Where(w => w.DurationInMinutes <= request.Duration);

        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(w => w.Name.Contains(request.Search));

        var workouts = await query
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
