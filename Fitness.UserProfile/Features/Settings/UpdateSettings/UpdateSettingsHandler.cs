using BuildingBlocks.Models;
using Fitness.UserProfile.Common;
using Fitness.UserProfile.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Fitness.UserProfile.Features.Settings.UpdateSettings;

public class UpdateSettingsHandler(UserProfileDbContext db, ICurrentUser currentUser)
    : IRequestHandler<UpdateSettingsCommand, ApiResponse<SettingsResponse>>
{
    public async Task<ApiResponse<SettingsResponse>> Handle(UpdateSettingsCommand request, CancellationToken cancellationToken)
    {
        if (currentUser.UserId is not int userId)
            return ApiResponse<SettingsResponse>.Failure("User is not authenticated.", HttpStatusCode.Unauthorized);

        var profile = await db.UserProfiles
            .Include(p => p.UserSettings)
            .Include(p => p.NotificationSettings)
            .Include(p => p.PrivacySettings)
            .FirstOrDefaultAsync(p => p.Id == userId, cancellationToken);

        if (profile is null)
            return ApiResponse<SettingsResponse>.Failure("Profile not found.", HttpStatusCode.NotFound);

        if (request.Preferences is { } prefs)
        {
            if (prefs.Language is not null) profile.UserSettings.Language = prefs.Language;
            if (prefs.Theme is not null) profile.UserSettings.Theme = prefs.Theme;
            if (prefs.WeightUnit is not null) profile.UserSettings.WeightUnit = prefs.WeightUnit;
            if (prefs.HeightUnit is not null) profile.UserSettings.HeightUnit = prefs.HeightUnit;
            if (prefs.DistanceUnit is not null) profile.UserSettings.DistanceUnit = prefs.DistanceUnit;
        }

        if (request.Notifications is { } notif)
        {
            if (notif.WorkoutReminders is not null) profile.NotificationSettings.WorkoutReminders = notif.WorkoutReminders.Value;
            if (notif.MealReminders is not null) profile.NotificationSettings.MealReminders = notif.MealReminders.Value;
            if (notif.AchievementAlerts is not null) profile.NotificationSettings.AchievementAlerts = notif.AchievementAlerts.Value;
            if (notif.WeeklyReports is not null) profile.NotificationSettings.WeeklyReports = notif.WeeklyReports.Value;
            if (notif.EmailNotifications is not null) profile.NotificationSettings.EmailNotifications = notif.EmailNotifications.Value;
            if (notif.PushNotifications is not null) profile.NotificationSettings.PushNotifications = notif.PushNotifications.Value;
        }

        if (request.Privacy is { } privacy)
        {
            if (privacy.ProfileVisibility is not null) profile.PrivacySettings.ProfileVisibility = privacy.ProfileVisibility;
            if (privacy.ShowProgressToFriends is not null) profile.PrivacySettings.ShowProgressToFriends = privacy.ShowProgressToFriends.Value;
            if (privacy.AllowDataSharing is not null) profile.PrivacySettings.AllowDataSharing = privacy.AllowDataSharing.Value;
        }

        await db.SaveChangesAsync(cancellationToken);

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
