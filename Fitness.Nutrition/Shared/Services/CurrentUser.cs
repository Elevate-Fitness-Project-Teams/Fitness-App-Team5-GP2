using Fitness.Nutrition.Shared.Interfaces;
using System.Security.Claims;

namespace Fitness.Nutrition.Shared.Services;

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
