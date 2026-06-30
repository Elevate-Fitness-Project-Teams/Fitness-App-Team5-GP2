using FluentValidation;

namespace Fitness.CalculationEngine.Features.UserFitnessStats.CheckUserFitnessStateIsExist;

public class CheckUserFitnessStateIsExistQueryValidator:AbstractValidator<CheckUserFitnessStateIsExistQuery>
{
    public CheckUserFitnessStateIsExistQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id Is Required");
    }
}
