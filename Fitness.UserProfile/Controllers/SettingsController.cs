using Fitness.UserProfile.Features.Settings.GetSettings;
using Fitness.UserProfile.Features.Settings.UpdateSettings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.UserProfile.Controllers;

[ApiController]
[Route("api/v1/settings")]
public class SettingsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetSettings(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetSettingsQuery(), cancellationToken);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSettings([FromBody] UpdateSettingsCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return StatusCode((int)result.StatusCode, result);
    }
}
