using Fitness.Workout.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Workout.Data.Configurations;

public class WorkoutConfiguration : IEntityTypeConfiguration<Workout.Domain.Entities.Workout>
{
    public void Configure(EntityTypeBuilder<Workout.Domain.Entities.Workout> builder)
    {
        builder.HasKey(w => w.WorkoutId);

        builder.Property(w => w.PlanId)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(w => w.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(w => w.Category)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(w => w.Category);

        builder.Property(w => w.DurationInMinutes)
            .IsRequired();

        builder.Property(w => w.Difficulty)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(w => w.CaloriesBurn)
            .IsRequired();

        builder.Property(w => w.ImageUrl)
            .HasMaxLength(500);

        builder.Property(w => w.IsPremium)
            .HasDefaultValue(false);

        builder.HasOne(w => w.WorkoutPlan)
            .WithMany(p => p.Workouts)
            .HasForeignKey(w => w.PlanId);
    }
}
