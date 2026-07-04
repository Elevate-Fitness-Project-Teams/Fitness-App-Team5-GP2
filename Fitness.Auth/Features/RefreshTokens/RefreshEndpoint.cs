using Fitness.Auth.Shared.Interfaces;
using MediatR;

namespace Fitness.Auth.Features.RefreshTokens;

public class RefreshEndpoint :IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/auth/refresh", async (RefreshCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            return result.Success
                ? Results.Ok(result.Result)
                : Results.Unauthorized();
        })
        .WithName("Refresh")
        .WithTags("Auth")
        .Produces<RefreshResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);
    }
}
