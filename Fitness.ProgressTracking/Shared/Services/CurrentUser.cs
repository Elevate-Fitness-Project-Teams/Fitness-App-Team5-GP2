using Fitness.ProgressTracking.Shared.Interfaces;
using System.Security.Claims;

namespace Fitness.ProgressTracking.Shared.Services;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        UserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue("userId") ??"";
    }
    public string UserId { get ; set ; }
}
