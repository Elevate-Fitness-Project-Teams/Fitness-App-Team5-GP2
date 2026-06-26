using Fitness.UserProfile.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.UserProfile.Data.Configurations;

public class NotificationSettingsConfiguration : IEntityTypeConfiguration<NotificationSettings>
{
    public void Configure(EntityTypeBuilder<NotificationSettings> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.WorkoutReminders)
            .HasDefaultValue(true);

        builder.Property(s => s.MealReminders)
            .HasDefaultValue(true);

        builder.Property(s => s.AchievementAlerts)
            .HasDefaultValue(true);

        builder.Property(s => s.WeeklyReports)
            .HasDefaultValue(true);

        builder.Property(s => s.EmailNotifications)
            .HasDefaultValue(true);

        builder.Property(s => s.PushNotifications)
            .HasDefaultValue(true);
    }
}
