using Microsoft.AspNetCore.Http;

namespace Fitness.UserProfile.Common;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    // TODO (security): this trusts the X-User-Id header directly.
    // Safe ONLY behind an API Gateway that validates the JWT and injects this header.
    // Replace with JWT claim reading once Auth issues tokens / the Gateway exists.
    public int? UserId
    {
        get
        {
            var header = httpContextAccessor.HttpContext?.Request.Headers["X-User-Id"].FirstOrDefault();
            return int.TryParse(header, out var id) ? id : null;
        }
    }
}
