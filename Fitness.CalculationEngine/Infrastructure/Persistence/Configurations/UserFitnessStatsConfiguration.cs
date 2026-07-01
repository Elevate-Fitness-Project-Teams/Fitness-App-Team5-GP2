using Fitness.CalculationEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.CalculationEngine.Infrastructure.Persistence.Configurations;

public class UserFitnessStatsConfiguration : IEntityTypeConfiguration<UserFitnessStat>
{
    public void Configure(EntityTypeBuilder<UserFitnessStat> builder)
    {
        builder.HasIndex(s => s.UserId);

        builder.Property(s => s.RecordedAt);
    }
}
