using BuildingBlocks.Models;
using MediatR;

namespace Fitness.UserProfile.Features.Settings.GetSettings;

public record GetSettingsQuery : IRequest<ApiResponse<SettingsResponse>>;
