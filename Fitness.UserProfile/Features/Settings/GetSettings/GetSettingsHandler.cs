using BuildingBlocks.Models;
using Fitness.UserProfile.Common;
using Fitness.UserProfile.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Fitness.UserProfile.Features.Settings.GetSettings;

public class GetSettingsHandler(UserProfileDbContext db, ICurrentUser currentUser)
    : IRequestHandler<GetSettingsQuery, ApiResponse<SettingsResponse>>
{
    public async Task<ApiResponse<SettingsResponse>> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
    {
        if (currentUser.UserId is not int userId)
            return ApiResponse<SettingsResponse>.Failure("User is not authenticated.", HttpStatusCode.Unauthorized);

        var profile = await db.UserProfiles
            .AsNoTracking()
            .Include(p => p.UserSettings)
            .Include(p => p.NotificationSettings)
            .Include(p => p.PrivacySettings)
            .FirstOrDefaultAsync(p => p.Id == userId, cancellationToken);

        if (profile is null)
            return ApiResponse<SettingsResponse>.Failure("Profile not found.", HttpStatusCode.NotFound);

        var response = new SettingsResponse(
            new PreferencesDto(
                profile.UserSettings.Language,
                profile.UserSettings.Theme,
                profile.UserSettings.WeightUnit,
                profile.UserSettings.HeightUnit,
                profile.UserSettings.DistanceUnit),
            new NotificationSettingsDto(
                profile.NotificationSettings.WorkoutReminders,
                profile.NotificationSettings.MealReminders,
                profile.NotificationSettings.AchievementAlerts,
                profile.NotificationSettings.WeeklyReports,
                profile.NotificationSettings.EmailNotifications,
                profile.NotificationSettings.PushNotifications),
            new PrivacySettingsDto(
                profile.PrivacySettings.ProfileVisibility,
                profile.PrivacySettings.ShowProgressToFriends,
                profile.PrivacySettings.AllowDataSharing));

        return ApiResponse<SettingsResponse>.Successed(response);
    }
}
