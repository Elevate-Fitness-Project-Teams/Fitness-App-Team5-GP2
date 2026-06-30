using BuildingBlocks.Models;
using MediatR;

namespace Fitness.UserProfile.Features.Profiles.UploadProfilePicture;

public record UploadProfilePictureCommand(
    byte[] Content,
    string FileName,
    string ContentType,
    long Length) : IRequest<ApiResponse<UploadProfilePictureResponse>>;

public record UploadProfilePictureResponse(string ProfilePictureUrl);
