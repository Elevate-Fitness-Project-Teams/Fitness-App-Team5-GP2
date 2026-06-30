using Fitness.CalculationEngine.Domain.Enums;
using FluentValidation;

namespace Fitness.CalculationEngine.Features.UserFitnessStats.WeightGoalActivity;

public class WeightGoalActivityCommandValidator :AbstractValidator<WeightGoalActivityCommand>
{
    public WeightGoalActivityCommandValidator()
    {
        RuleFor(x => x.Height)
             .ExclusiveBetween(140,220)
             .WithMessage("Height must be Greater than 140 and less than 220 cm");

        RuleFor(x => x.Weight)
            .ExclusiveBetween(40,200)
            .WithMessage("Weight must be between 40 and 200 kg");

        RuleFor(x => x.Age)
          .ExclusiveBetween(16, 100)
          .WithMessage("Age must be between 16 and 100 kg");

        RuleFor(x => x.ActivityLevel)
            .Must(value => Enum.TryParse<ActivityLevel>(value, true, out _))
            .WithMessage("Activity level must be one of: Rookie, Beginner, Intermediate, Advance, TrueBeast");

        RuleFor(x => x.Goal)
            .Must(value => Enum.TryParse<Goal>(value, true, out _))
            .WithMessage("Invalid Goal");

        RuleFor(x => x.Gender)
           .Must(value => Enum.TryParse<Gender>(value, true, out _))
           .WithMessage("Invalid Gender");

        RuleFor(x => x.UserId)
            .NotNull().NotEmpty()
            .WithMessage("User Id Is Required");



    }
}
