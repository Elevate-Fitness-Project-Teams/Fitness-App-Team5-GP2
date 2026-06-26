using Fitness.ProgressTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.ProgressTracking.Data.Configurations;

public class WorkoutLogConfiguration : IEntityTypeConfiguration<WorkoutLog>
{
    public void Configure(EntityTypeBuilder<WorkoutLog> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.UserId)
            .IsRequired();

        builder.HasIndex(l => l.UserId);

        builder.Property(l => l.WorkoutId)
            .IsRequired();

        builder.Property(l => l.SessionId)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(l => l.SessionId);

        builder.Property(l => l.DurationInMinutes)
            .IsRequired();

        builder.Property(l => l.CaloriesBurned)
            .IsRequired();

        builder.Property(l => l.Rating)
            .IsRequired();

        builder.Property(l => l.Notes)
            .HasMaxLength(1000);

        builder.Property(l => l.CompletedAt)
            .IsRequired();
    }
}
