namespace Fitness.UserProfile.Features.Profiles.GetProfile;

public record ProfileResponse(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string? ProfilePictureUrl,
    bool IsPremiumCached,
    DateTime MemberSince);
