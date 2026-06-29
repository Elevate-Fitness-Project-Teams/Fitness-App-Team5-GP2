using FluentValidation;

namespace Fitness.Workout.Features.Queries.GetWorkoutById;

public class GetWorkoutByIdQueryValidator : AbstractValidator<GetWorkoutByIdQuery>
{
    public GetWorkoutByIdQueryValidator()
    {
        RuleFor(x => x.WorkoutId).GreaterThan(0);
    }
}
