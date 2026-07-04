namespace Fitness.Auth.Domain.Entities;

public class LoginAttempt
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;
    public bool IsSuccess { get; set; }
    public string IpAddress { get; set; } = string.Empty;
}
