using Microsoft.AspNetCore.Identity;

namespace Fitness.Auth.Domain.Entities;

public class User :IdentityUser<Guid>
{
    public DateTime? LockedUntil { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}
