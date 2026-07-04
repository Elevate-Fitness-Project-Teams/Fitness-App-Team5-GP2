using FluentValidation;

namespace Fitness.Auth.Features.RefreshTokens;

public class RefreshValidator : AbstractValidator<RefreshCommand>
{
    public RefreshValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}