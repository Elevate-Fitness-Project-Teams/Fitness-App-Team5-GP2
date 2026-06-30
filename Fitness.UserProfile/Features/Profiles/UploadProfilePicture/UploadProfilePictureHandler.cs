using BuildingBlocks.Models;
using Fitness.UserProfile.Common;
using Fitness.UserProfile.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Fitness.UserProfile.Features.Profiles.UploadProfilePicture;

public class UploadProfilePictureHandler(
    UserProfileDbContext db,
    ICurrentUser currentUser,
    IWebHostEnvironment environment)
    : IRequestHandler<UploadProfilePictureCommand, ApiResponse<UploadProfilePictureResponse>>
{
    public async Task<ApiResponse<UploadProfilePictureResponse>> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
    {
        if (currentUser.UserId is not int userId)
            return ApiResponse<UploadProfilePictureResponse>.Failure("User is not authenticated.", HttpStatusCode.Unauthorized);

        var profile = await db.UserProfiles.FirstOrDefaultAsync(p => p.Id == userId, cancellationToken);
        if (profile is null)
            return ApiResponse<UploadProfilePictureResponse>.Failure("Profile not found.", HttpStatusCode.NotFound);

        var extension = request.ContentType == "image/png" ? ".png" : ".jpg";

        var webRoot = environment.WebRootPath
                      ?? Path.Combine(environment.ContentRootPath, "wwwroot");
        var folder = Path.Combine(webRoot, "images", "profiles");
        Directory.CreateDirectory(folder);

        var fileName = $"{userId}{extension}";
        var fullPath = Path.Combine(folder, fileName);
        await File.WriteAllBytesAsync(fullPath, request.Content, cancellationToken);

        var url = $"/images/profiles/{fileName}";
        profile.ProfilePictureUrl = url;
        await db.SaveChangesAsync(cancellationToken);

        return ApiResponse<UploadProfilePictureResponse>.Successed(new UploadProfilePictureResponse(url));
    }
}
