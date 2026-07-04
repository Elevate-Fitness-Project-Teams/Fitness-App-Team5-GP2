using FluentValidation;

namespace Fitness.Auth.Features.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);

        // Identity's PasswordOptions enforces the deeper policy (digit, uppercase, etc.)
        // at CreateAsync time. This is a cheap early rejection to avoid a round trip
        // for obviously-too-short input.
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}