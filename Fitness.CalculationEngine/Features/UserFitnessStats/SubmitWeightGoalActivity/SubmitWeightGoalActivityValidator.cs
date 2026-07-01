using Fitness.CalculationEngine.Domain.Enums;
using Fitness.CalculationEngine.Domain.Services;
using FluentValidation;

namespace Fitness.CalculationEngine.Features.UserFitnessStats.WeightGoalActivity;

public class SubmitWeightGoalActivityValidator :AbstractValidator<SubmitWeightGoalActivityCommand>
{
    public SubmitWeightGoalActivityValidator()
    {
        RuleFor(x => x.UserId).NotEmpty()
            .WithMessage("A valid authenticated user is required.");
        
        RuleFor(x => x.Weight)
            .InclusiveBetween(40, 200)
            .WithMessage("Weight must be between 40 and 200 kg.");
        
        RuleFor(x => x.Height)
            .InclusiveBetween(140, 220)
            .WithMessage("Height must be between 140 and 220 cm.");
        
        RuleFor(x => x.Age)
            .InclusiveBetween(16, 100)
            .WithMessage("Age must be between 16 and 100.");
        
        RuleFor(x => x.Gender)
            .NotEmpty()
            .Must(BeAValidEnumValue<Gender>)
            .WithMessage($"Gender must be one of: {string.Join(", ", Enum.GetNames<Gender>())}.");
        
        RuleFor(x => x.Goal)
            .NotEmpty()
            .Must(BeAValidGoal)
            .WithMessage("Goal must be one of: Lose Weight, Get Fitter, Gain Weight, Gain More Flexible, Learn the Basic.");
        
        RuleFor(x => x.ActivityLevel)
            .NotEmpty()
            .Must(BeAValidEnumValue<ActivityLevel>)
            .WithMessage($"ActivityLevel must be one of: {string.Join(", ", Enum.GetNames<ActivityLevel>())}.");
    }

    private static bool BeAValidEnumValue<TEnum>(string value) where TEnum : struct, Enum =>
        Enum.TryParse<TEnum>(value, ignoreCase: true, out _);
    private static bool BeAValidGoal(string value) => FitnessGoalParser.TryParse(value, out _);
}

