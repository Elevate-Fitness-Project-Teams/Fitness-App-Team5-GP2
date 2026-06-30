using BuildingBlocks.Models;
using Fitness.UserProfile.Features.Profiles.GetProfile;
using Fitness.UserProfile.Features.Profiles.UpdateProfile;
using Fitness.UserProfile.Features.Profiles.UploadProfilePicture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fitness.UserProfile.Controllers;

[ApiController]
[Route("api/v1/profile")]
public class ProfileController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProfileQuery(), cancellationToken);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost("picture")]
    public async Task<IActionResult> UploadPicture(IFormFile? profilePicture, CancellationToken cancellationToken)
    {
        if (profilePicture is null || profilePicture.Length == 0)
            return StatusCode(
                (int)HttpStatusCode.BadRequest,
                ApiResponse<object>.Failure("A profile picture file is required.", HttpStatusCode.BadRequest));

        using var memoryStream = new MemoryStream();
        await profilePicture.CopyToAsync(memoryStream, cancellationToken);

        var command = new UploadProfilePictureCommand(
            memoryStream.ToArray(),
            profilePicture.FileName,
            profilePicture.ContentType,
            profilePicture.Length);

        var result = await mediator.Send(command, cancellationToken);
        return StatusCode((int)result.StatusCode, result);
    }
}
