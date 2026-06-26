using Fitness.CalculationEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.CalculationEngine.Data.Configurations;

public class CalculatedMetricsConfiguration : IEntityTypeConfiguration<CalculatedMetrics>
{
    public void Configure(EntityTypeBuilder<CalculatedMetrics> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.UserId)
            .IsRequired();

        builder.HasIndex(m => m.UserId)
            .IsUnique();

        builder.Property(m => m.Bmr)
            .IsRequired();

        builder.Property(m => m.Tdee)
            .IsRequired();

        builder.Property(m => m.CalorieTarget)
            .IsRequired();

        builder.Property(m => m.Status)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(m => m.CalculatedAt)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
