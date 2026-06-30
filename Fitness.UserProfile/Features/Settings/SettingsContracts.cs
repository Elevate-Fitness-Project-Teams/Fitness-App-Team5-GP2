namespace Fitness.UserProfile.Features.Settings;

// Grouped read model returned by GET /api/v1/settings
public record SettingsResponse(
    PreferencesDto Preferences,
    NotificationSettingsDto Notifications,
    PrivacySettingsDto Privacy);

public record PreferencesDto(
    string Language,
    string Theme,
    string WeightUnit,
    string HeightUnit,
    string DistanceUnit);

public record NotificationSettingsDto(
    bool WorkoutReminders,
    bool MealReminders,
    bool AchievementAlerts,
    bool WeeklyReports,
    bool EmailNotifications,
    bool PushNotifications);

public record PrivacySettingsDto(
    string ProfileVisibility,
    bool ShowProgressToFriends,
    bool AllowDataSharing);
