using BuildingBlocks.Models;
using Fitness.UserProfile.Features.Profiles.GetProfile;
using MediatR;

namespace Fitness.UserProfile.Features.Profiles.UpdateProfile;

public record UpdateProfileCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber) : IRequest<ApiResponse<ProfileResponse>>;
