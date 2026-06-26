using Fitness.Workout.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Workout.Data.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(e => e.ExerciseId);

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(e => e.TargetMuscles)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.Equipment)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(e => e.Difficulty)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.VideoUrl)
            .HasMaxLength(500);
    }
}
