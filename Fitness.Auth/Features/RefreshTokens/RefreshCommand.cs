using Fitness.Auth.Domain.Entities;
using Fitness.Auth.Infrastructure.Persistence.DbContexts;
using Fitness.Auth.Shared.Models;
using Fitness.Auth.Shared.Repositories;
using Fitness.Auth.Shared.Responses;
using Fitness.Auth.Shared.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Fitness.Auth.Features.RefreshTokens;

public record RefreshCommand(string RefreshToken) : IRequest<RequestResult<RefreshResponse>>;

public record RefreshResponse(string AccessToken, string RefreshToken, DateTimeOffset AccessTokenExpiresAt);

public class RefreshHandler(
    Repository<RefreshToken> repository,
    UserManager<User> userManager,
    TokenService tokenService,
    IOptions<JwtOptions> jwtOptions,
    ILogger<RefreshHandler> logger)
    : IRequestHandler<RefreshCommand, RequestResult<RefreshResponse>>
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<RequestResult<RefreshResponse>> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var incomingHash = tokenService.HashToken(request.RefreshToken);

        // Read-only lookup: AsNoTracking per convention. Active-window check
        // (not revoked, not expired) happens in the query itself, not after.
        var storedToken = await repository.Get(rt => rt.Token == incomingHash
                      && rt.RevokedAt == null
                      && rt.ExpiresAt > DateTimeOffset.UtcNow)
                      .Select(rt => new
                      {
                          rt.UserId,
                          rt.ExpiresAt,
                          rt.Token
                      })
                     .FirstOrDefaultAsync(cancellationToken);

        if (storedToken is null)
        {
            logger.LogInformation("Refresh rejected: token not found, expired, or revoked");
            return RequestResult<RefreshResponse>.Failure(ResultCode.RefreshTokenInvalid);
        }

        var user = await userManager.FindByIdAsync(storedToken.UserId.ToString());
        if (user is null)
        {
            logger.LogWarning("Refresh rejected: token valid but user {UserId} no longer exists", storedToken.UserId);
            return RequestResult<RefreshResponse>.Failure(ResultCode.RefreshTokenInvalid);
        }

        var newAccessToken = tokenService.GenerateAccessToken(user);

        logger.LogInformation("Access token refreshed for user {UserId}", user.Id);

        // No rotation in v1 (agreed trade-off): the same refresh token remains valid
        // until its own ExpiresAt. Returned here only so the client's token-handling
        // code has one consistent response shape between Login and Refresh.
        return RequestResult<RefreshResponse>.succeeded(new RefreshResponse(
            newAccessToken,
            request.RefreshToken,
            DateTimeOffset.UtcNow.AddMinutes(_jwtOptions.AccessTokenLifetimeMinutes)),ResultCode.RefreshSuccess);
    }
}



