using FluentValidation;

namespace Fitness.Workout.Features.Queries.GetWorkouts;

public class GetWorkoutsQueryValidator : AbstractValidator<GetWorkoutsQuery>
{
    public GetWorkoutsQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 50);
    }
}
