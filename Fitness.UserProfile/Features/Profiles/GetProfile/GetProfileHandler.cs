using BuildingBlocks.Models;
using Fitness.UserProfile.Common;
using Fitness.UserProfile.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Fitness.UserProfile.Features.Profiles.GetProfile;

public class GetProfileHandler(UserProfileDbContext db, ICurrentUser currentUser)
    : IRequestHandler<GetProfileQuery, ApiResponse<ProfileResponse>>
{
    public async Task<ApiResponse<ProfileResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        if (currentUser.UserId is not int userId)
            return ApiResponse<ProfileResponse>.Failure("User is not authenticated.", HttpStatusCode.Unauthorized);

        var profile = await db.UserProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == userId, cancellationToken);

        if (profile is null)
            return ApiResponse<ProfileResponse>.Failure("Profile not found.", HttpStatusCode.NotFound);

        var response = new ProfileResponse(
            profile.Id,
            profile.FirstName,
            profile.LastName,
            profile.Email,
            profile.PhoneNumber,
            profile.ProfilePictureUrl,
            profile.IsPremiumCached,
            profile.MemberSince);

        return ApiResponse<ProfileResponse>.Successed(response);
    }
}
