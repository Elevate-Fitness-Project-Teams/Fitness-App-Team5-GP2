using FluentValidation;

namespace Fitness.CalculationEngine.Features.UserFitnessStats.GetUserFitnessStats;

public class GetUserFitnessStatsValidator :AbstractValidator<GetUserFitnessStatsQuery>
{
    public GetUserFitnessStatsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id Is Required");
    }
}
