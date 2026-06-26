using Fitness.ProgressTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.ProgressTracking.Data.Configurations;

public class WorkoutLogExerciseConfiguration : IEntityTypeConfiguration<WorkoutLogExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutLogExercise> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ExerciseId)
            .IsRequired();

        builder.Property(e => e.SetsCompleted)
            .IsRequired();

        builder.Property(e => e.RepsCompleted)
            .IsRequired();

        builder.Property(e => e.WeightUsed)
            .HasDefaultValue(0.0);

        builder.Property(e => e.Completed)
            .IsRequired();

        builder.HasOne(e => e.WorkoutLog)
            .WithMany(l => l.WorkoutLogExercises)
            .HasForeignKey(e => e.WorkoutLogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
