using Fitness.Auth.Domain.Entities;
using Fitness.Auth.Shared.Responses;
using Fitness.Shared.Messaging.Auth;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Fitness.Auth.Features.Register;

public record RegisterCommand(string Email, string Password , string FirstName , string LastName ,string PhoneNumber) : IRequest<RequestResult<RegisterResponse>>;

public record RegisterResponse(Guid UserId, string Email);
public class RegisterHandler(
    UserManager<User> userManager,
    ILogger<RegisterHandler> logger ,IPublishEndpoint publishEndpoint)
    : IRequestHandler<RegisterCommand, RequestResult<RegisterResponse>>
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

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
       await _publishEndpoint.Publish(new UserRegisteredEvent(user.Id, user.Email!,request.FirstName,request.LastName,request.PhoneNumber
           ), cancellationToken);
        logger.LogInformation("User {UserId} registered", user.Id);
        return RequestResult<RegisterResponse>.succeeded(new RegisterResponse(user.Id, user.Email!),ResultCode.RegistrationSuccess);
    }
}


