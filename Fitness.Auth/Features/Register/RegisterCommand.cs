using BuildingBlocks.Models;
using Fitness.Auth.Domain.Entities;
using Fitness.Auth.Shared.Responses;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Fitness.Auth.Features.Register;

public record RegisterCommand(string Email, string Password) : IRequest<RequestResult<RegisterResponse>>;

public record RegisterResponse(Guid UserId, string Email);

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

public class RegisterHandler(
    UserManager<User> userManager,
    ILogger<RegisterHandler> logger)
    : IRequestHandler<RegisterCommand, RequestResult<RegisterResponse>>
{
    public async Task<RequestResult<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Idempotency check before any write, per platform convention.
        var existing = await userManager.FindByEmailAsync(request.Email);
        if (existing is not null)
        {
            logger.LogInformation("Register rejected: email already exists");
            return RequestResult<RegisterResponse>.Failure(ResultCode.DuplicateEmail);
        }

        var user = new User
        {
            UserName = request.Email,
            Email = request.Email
        };

        var createResult = await userManager.CreateAsync(user, request.Password);

        if (!createResult.Succeeded)
        {
            var detail = string.Join("; ", createResult.Errors.Select(e => e.Description));
            logger.LogWarning("Register failed for identity policy reasons: {Detail}", detail);
            return RequestResult<RegisterResponse>.Failure(ResultCode.WeakPassword);
        }

        logger.LogInformation("User {UserId} registered", user.Id);
        return RequestResult<RegisterResponse>.succeeded(new RegisterResponse(user.Id, user.Email!),ResultCode.RegistrationSuccess);
    }
}

public static class RegisterEndpoint
{
    public static IEndpointRouteBuilder MapRegisterEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/auth/register", async (RegisterCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            return result.Success
                ? Results.Created($"/api/v1/users/{result.Result.UserId}", result.Result)
                : result.Code switch
                {
                    ResultCode.DuplicateEmail => Results.Conflict(ApiResponse<RegisterResponse>.Failure (result.Code.ToString())),
                    ResultCode.WeakPassword => Results.BadRequest(ApiResponse<RegisterResponse>.Failure(result.Code.ToString())),
                    _ => Results.BadRequest(ApiResponse<RegisterResponse>.Failure(result.Code.ToString()))
                };
        })
        .WithName("Register")
        .WithTags("Auth")
        .Produces<ApiResponse<RegisterResponse>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status400BadRequest);

        return app;
    }
}
