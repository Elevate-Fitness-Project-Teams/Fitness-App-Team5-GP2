using BuildingBlocks.Models;
using Fitness.Auth.Shared.Interfaces;
using Fitness.Auth.Shared.Responses;
using MediatR;

namespace Fitness.Auth.Features.Register;

public class RegisterEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/auth/register", async (RegisterCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            return result.Success
                ? Results.Created($"/api/v1/users/{result.Result.UserId}", result.Result)
                : result.Code switch
                {
                    ResultCode.DuplicateEmail => Results.Conflict(ApiResponse<RegisterResponse>.Failure(result.Code.ToString())),
                    ResultCode.WeakPassword => Results.BadRequest(ApiResponse<RegisterResponse>.Failure(result.Code.ToString())),
                    _ => Results.BadRequest(ApiResponse<RegisterResponse>.Failure(result.Code.ToString()))
                };
        })
        .WithName("Register")
        .WithTags("Auth")
        .Produces<ApiResponse<RegisterResponse>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status400BadRequest);
    }
}