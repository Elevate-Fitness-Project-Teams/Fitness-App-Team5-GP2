using Fitness.ProgressTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.ProgressTracking.Infrastructure.Persistence.Configurations;

public class WorkoutLogExerciseConfiguration : IEntityTypeConfiguration<WorkoutLogExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutLogExercise> builder)
    {

        builder.HasOne(e => e.WorkoutLog)
            .WithMany(l => l.WorkoutLogExercises)
            .HasForeignKey(e => e.WorkoutLogId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
