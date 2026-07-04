using Fitness.Auth.Domain.Entities;
using Fitness.Auth.Shared.Models;
using Fitness.Auth.Shared.Repositories;
using Fitness.Auth.Shared.Responses;
using Fitness.Auth.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Fitness.Auth.Features.Login;

public record LoginCommand(string Email, string Password) : IRequest<RequestResult<LoginResponse>>;

public record LoginResponse(string AccessToken, string RefreshToken, DateTimeOffset AccessTokenExpiresAt);

public class LoginHandler(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    Repository<RefreshToken> repository,
    TokenService tokenService,
    IOptions<JwtOptions> jwtOptions,
    ILogger<LoginHandler> logger)
    : IRequestHandler<LoginCommand, RequestResult<LoginResponse>>
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly Repository<RefreshToken> _repository = repository;

    public async Task<RequestResult<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            logger.LogInformation("Login rejected: no matching account");
            return RequestResult<LoginResponse>.Failure(ResultCode.InvalidCredentials);
        }

        var checkResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);

        if (checkResult.IsLockedOut)
        {
            logger.LogWarning("Login blocked: account {UserId} is locked out", user.Id);
            return RequestResult<LoginResponse>.Failure(ResultCode.AccountLockedOut);
        }

        if (!checkResult.Succeeded)
        {
            logger.LogInformation("Login rejected: bad password for account {UserId}", user.Id);
            return RequestResult<LoginResponse>.Failure(ResultCode.InvalidCredentials);
        }

        var accessToken = tokenService.GenerateAccessToken(user);
        var (refreshTokenPlaintext, refreshTokenHash) = tokenService.GenerateRefreshToken();
        var expiresAt = DateTimeOffset.UtcNow.AddDays(_jwtOptions.RefreshTokenLifetimeDays);

        _repository.Add(new RefreshToken
        {
            UserId = user.Id,
            Token = refreshTokenHash,
            ExpiresAt = expiresAt.Date
        });
        await _repository.SaveChangeAsync(cancellationToken);

        logger.LogInformation("User {UserId} logged in", user.Id);

        return RequestResult<LoginResponse>.succeeded(new LoginResponse(
            accessToken,
            refreshTokenPlaintext,
            DateTimeOffset.UtcNow.AddMinutes(_jwtOptions.AccessTokenLifetimeMinutes)),ResultCode.LoginSuccess);
    }
}

