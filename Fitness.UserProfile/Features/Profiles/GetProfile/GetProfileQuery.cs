using BuildingBlocks.Models;
using MediatR;

namespace Fitness.UserProfile.Features.Profiles.GetProfile;

public record GetProfileQuery : IRequest<ApiResponse<ProfileResponse>>;
