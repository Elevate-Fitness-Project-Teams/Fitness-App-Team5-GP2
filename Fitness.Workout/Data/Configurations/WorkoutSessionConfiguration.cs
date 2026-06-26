using Fitness.Workout.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Workout.Data.Configurations;

public class WorkoutSessionConfiguration : IEntityTypeConfiguration<WorkoutSession>
{
    public void Configure(EntityTypeBuilder<WorkoutSession> builder)
    {
        builder.HasKey(s => s.SessionId);

        builder.Property(s => s.SessionId)
            .HasMaxLength(100);

        builder.Property(s => s.UserId)
            .IsRequired();

        builder.HasIndex(s => s.UserId);

        builder.Property(s => s.StartedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(s => s.Status)
            .HasMaxLength(20)
            .HasDefaultValue("Active");

        builder.HasOne(s => s.Workout)
            .WithMany(w => w.WorkoutSessions)
            .HasForeignKey(s => s.WorkoutId);
    }
}
