using BuildingBlocks.Models;
using MediatR;

namespace Fitness.UserProfile.Features.Settings.UpdateSettings;

// All fields are nullable => PARTIAL update. Only provided (non-null) fields are applied.
public record UpdateSettingsCommand(
    UpdatePreferencesDto? Preferences,
    UpdateNotificationsDto? Notifications,
    UpdatePrivacyDto? Privacy) : IRequest<ApiResponse<SettingsResponse>>;

public record UpdatePreferencesDto(
    string? Language,
    string? Theme,
    string? WeightUnit,
    string? HeightUnit,
    string? DistanceUnit);

public record UpdateNotificationsDto(
    bool? WorkoutReminders,
    bool? MealReminders,
    bool? AchievementAlerts,
    bool? WeeklyReports,
    bool? EmailNotifications,
    bool? PushNotifications);

public record UpdatePrivacyDto(
    string? ProfileVisibility,
    bool? ShowProgressToFriends,
    bool? AllowDataSharing);
