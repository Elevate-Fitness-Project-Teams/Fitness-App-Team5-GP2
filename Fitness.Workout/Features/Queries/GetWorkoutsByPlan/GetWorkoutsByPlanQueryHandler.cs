using System.Net;
using BuildingBlocks.Models;
using Fitness.Workout.Features.Queries.GetWorkouts;
using Fitness.Workout.Repositories;
using MediatR;

namespace Fitness.Workout.Features.Queries.GetWorkoutsByPlan;

public class GetWorkoutsByPlanQueryHandler(IWorkoutRepository repository)
    : IRequestHandler<GetWorkoutsByPlanQuery, ApiResponse<List<WorkoutListDto>>>
{
    public async Task<ApiResponse<List<WorkoutListDto>>> Handle(
        GetWorkoutsByPlanQuery request,
        CancellationToken cancellationToken)
    {
        var planExists = await repository.PlanExistsAsync(request.PlanId, cancellationToken);

        if (!planExists)
            return ApiResponse<List<WorkoutListDto>>.Failure("RES_PLAN_NOT_FOUND", HttpStatusCode.NotFound);

        var workouts = await repository.GetByPlanIdAsync(request.PlanId, cancellationToken);

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
