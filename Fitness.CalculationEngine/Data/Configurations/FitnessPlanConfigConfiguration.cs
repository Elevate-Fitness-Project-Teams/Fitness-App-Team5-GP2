using Fitness.CalculationEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.CalculationEngine.Data.Configurations;

public class FitnessPlanConfigConfiguration : IEntityTypeConfiguration<FitnessPlanConfig>
{
    public void Configure(EntityTypeBuilder<FitnessPlanConfig> builder)
    {
        builder.HasKey(p => p.PlanId);

        builder.Property(p => p.PlanId)
            .HasMaxLength(50);

        builder.Property(p => p.PlanName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(p => p.Goal)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Status)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.MinCalorie)
            .IsRequired();

        builder.Property(p => p.MaxCalorie)
            .IsRequired();

        builder.Property(p => p.WorkoutsPerWeek)
            .IsRequired();

        builder.Property(p => p.EstimatedDuration)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.ProgramType)
            .HasMaxLength(50)
            .IsRequired();
    }
}
