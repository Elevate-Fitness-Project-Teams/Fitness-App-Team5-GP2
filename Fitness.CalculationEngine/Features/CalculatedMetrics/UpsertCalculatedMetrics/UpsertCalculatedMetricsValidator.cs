using FluentValidation;

namespace Fitness.CalculationEngine.Features.CalculatedMetrics.Calculate;

public class UpsertCalculatedMetricsValidator:AbstractValidator<UpsertCalculatedMetricsCommand>
{
    public UpsertCalculatedMetricsValidator()
    {
        RuleFor(x =>x.UserId).NotEmpty().WithMessage("UserId is required.");
    }
}
