using Fitness.CalculationEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.CalculationEngine.Infrastructure.Persistence.Configurations;

public class FitnessPlanConfigConfiguration : IEntityTypeConfiguration<FitnessPlanConfig>
{
    public void Configure(EntityTypeBuilder<FitnessPlanConfig> builder)
    {
    }
}
