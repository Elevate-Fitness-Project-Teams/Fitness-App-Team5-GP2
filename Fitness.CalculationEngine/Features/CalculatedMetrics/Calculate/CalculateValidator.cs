using FluentValidation;

namespace Fitness.CalculationEngine.Features.CalculatedMetrics.Calculate;

public class CalculateValidator:AbstractValidator<CalculateCommand>
{
    public CalculateValidator()
    {
        RuleFor(x =>x.UserId).NotEmpty().WithMessage("UserId is required.");
    }
}
