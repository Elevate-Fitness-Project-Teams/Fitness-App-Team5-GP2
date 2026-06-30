using Fitness.CalculationEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.CalculationEngine.Infrastructure.Persistence.Configurations;

public class UserPlanHistoryConfiguration : IEntityTypeConfiguration<UserPlanHistory>
{
    public void Configure(EntityTypeBuilder<UserPlanHistory> builder)
    {
        builder.HasIndex(h => h.UserId);
    }
}
