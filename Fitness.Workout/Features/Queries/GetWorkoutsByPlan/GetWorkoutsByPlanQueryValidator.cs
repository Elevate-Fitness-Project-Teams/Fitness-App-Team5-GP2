using FluentValidation;

namespace Fitness.Workout.Features.Queries.GetWorkoutsByPlan;

public class GetWorkoutsByPlanQueryValidator : AbstractValidator<GetWorkoutsByPlanQuery>
{
    public GetWorkoutsByPlanQueryValidator()
    {
        RuleFor(x => x.PlanId).NotEmpty();
    }
}
