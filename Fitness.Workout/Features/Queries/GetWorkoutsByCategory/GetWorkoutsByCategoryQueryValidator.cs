using FluentValidation;

namespace Fitness.Workout.Features.Queries.GetWorkoutsByCategory;

public class GetWorkoutsByCategoryQueryValidator : AbstractValidator<GetWorkoutsByCategoryQuery>
{
    private static readonly string[] ValidCategories =
    [
        "full-body", "chest", "arms", "shoulders", "back", "legs", "stomach"
    ];

    public GetWorkoutsByCategoryQueryValidator()
    {
        RuleFor(x => x.CategoryName)
            .NotEmpty()
            .Must(c => ValidCategories.Contains(c))
            .WithMessage("Invalid category");

        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 50);
    }
}
