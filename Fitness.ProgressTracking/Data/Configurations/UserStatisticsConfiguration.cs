using Fitness.ProgressTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.ProgressTracking.Data.Configurations;

public class UserStatisticsConfiguration : IEntityTypeConfiguration<UserStatistics>
{
    public void Configure(EntityTypeBuilder<UserStatistics> builder)
    {
        builder.HasKey(s => s.UserId);

        builder.Property(s => s.TotalWorkouts)
            .HasDefaultValue(0);

        builder.Property(s => s.TotalCaloriesBurned)
            .HasDefaultValue(0);

        builder.Property(s => s.TotalWeightLost)
            .HasDefaultValue(0.0);

        builder.Property(s => s.CurrentWeight)
            .HasDefaultValue(0.0);

        builder.Property(s => s.StartWeight)
            .HasDefaultValue(0.0);

        builder.Property(s => s.UpdatedAt)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
