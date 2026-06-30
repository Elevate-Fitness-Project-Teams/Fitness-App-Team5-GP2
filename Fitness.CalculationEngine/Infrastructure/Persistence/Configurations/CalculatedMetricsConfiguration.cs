using Fitness.CalculationEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.CalculationEngine.Infrastructure.Persistence.Configurations;

public class CalculatedMetricsConfiguration : IEntityTypeConfiguration<CalculatedMetrics>
{
    public void Configure(EntityTypeBuilder<CalculatedMetrics> builder)
    {

        builder.HasIndex(m => m.UserId)
            .IsUnique();
    }
}
