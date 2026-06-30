namespace Fitness.UserProfile.Common;

/// <summary>
/// Abstraction over "who is the current authenticated user".
/// Today it reads the X-User-Id header (injected by the API Gateway in the
/// documented design). When real JWT/Gateway is added, only the implementation
/// (CurrentUser) changes — handlers depend on this interface, not on the header.
/// </summary>
public interface ICurrentUser
{
    int? UserId { get; }
}
