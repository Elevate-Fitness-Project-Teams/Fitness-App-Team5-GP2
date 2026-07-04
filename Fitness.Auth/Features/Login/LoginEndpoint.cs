using Fitness.Auth.Shared.Interfaces;
using Fitness.Auth.Shared.Responses;
using MediatR;

namespace Fitness.Auth.Features.Login;

public class LoginEndpoint :IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/auth/login", async (LoginCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            return result.Success
                ? Results.Ok(result.Result)
                : result.Code switch
                {
                    ResultCode.AccountLockedOut => Results.Conflict("Account is LockedOut"),
                    ResultCode.InvalidCredentials => Results.Unauthorized(),
                    ResultCode.LoginSuccess => Results.Ok(result.Result),
                    _ => Results.Problem(result.Code.ToString())
                };
        })
        .WithName("Login")
        .WithTags("Auth")
        .Produces<LoginResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status423Locked);
    }
}