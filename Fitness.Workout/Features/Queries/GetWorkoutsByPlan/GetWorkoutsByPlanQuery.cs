using BuildingBlocks.Models;
using Fitness.Workout.Features.Queries.GetWorkouts;
using MediatR;

namespace Fitness.Workout.Features.Queries.GetWorkoutsByPlan;

public class GetWorkoutsByPlanQuery : IRequest<ApiResponse<List<WorkoutListDto>>>
{
    public string PlanId { get; set; } = string.Empty;

    public GetWorkoutsByPlanQuery(string planId)
    {
        PlanId = planId;
    }
}
