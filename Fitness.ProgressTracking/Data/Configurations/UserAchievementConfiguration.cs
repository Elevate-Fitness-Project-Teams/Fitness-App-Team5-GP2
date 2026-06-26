using Fitness.ProgressTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.ProgressTracking.Data.Configurations;

public class UserAchievementConfiguration : IEntityTypeConfiguration<UserAchievement>
{
    public void Configure(EntityTypeBuilder<UserAchievement> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.UserId)
            .IsRequired();

        builder.HasIndex(a => a.UserId);

        builder.Property(a => a.EarnedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(a => a.Achievement)
            .WithMany(a => a.UserAchievements)
            .HasForeignKey(a => a.AchievementId);
    }
}
