using Fitness.ProgressTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.ProgressTracking.Data.Configurations;

public class StreakConfiguration : IEntityTypeConfiguration<Streak>
{
    public void Configure(EntityTypeBuilder<Streak> builder)
    {
        builder.HasKey(s => s.UserId);

        builder.Property(s => s.CurrentStreak)
            .HasDefaultValue(0);

        builder.Property(s => s.LongestStreak)
            .HasDefaultValue(0);
    }
}
