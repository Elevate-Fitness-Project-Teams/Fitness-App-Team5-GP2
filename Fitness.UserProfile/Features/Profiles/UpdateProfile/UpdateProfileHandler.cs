using BuildingBlocks.Models;
using Fitness.UserProfile.Common;
using Fitness.UserProfile.Data;
using Fitness.UserProfile.Features.Profiles.GetProfile;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Fitness.UserProfile.Features.Profiles.UpdateProfile;

public class UpdateProfileHandler(UserProfileDbContext db, ICurrentUser currentUser)
    : IRequestHandler<UpdateProfileCommand, ApiResponse<ProfileResponse>>
{
    public async Task<ApiResponse<ProfileResponse>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        if (currentUser.UserId is not int userId)
            return ApiResponse<ProfileResponse>.Failure("User is not authenticated.", HttpStatusCode.Unauthorized);

        var profile = await db.UserProfiles.FirstOrDefaultAsync(p => p.Id == userId, cancellationToken);
        if (profile is null)
            return ApiResponse<ProfileResponse>.Failure("Profile not found.", HttpStatusCode.NotFound);

        profile.FirstName = request.FirstName;
        profile.LastName = request.LastName;
        profile.Email = request.Email;
        profile.PhoneNumber = request.PhoneNumber;

        await db.SaveChangesAsync(cancellationToken);

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
