using Fitness.CalculationEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.CalculationEngine.Infrastructure.Persistence.Configurations;

public class UserAssignedPlanConfiguration : IEntityTypeConfiguration<UserAssignedPlan>
{
    public void Configure(EntityTypeBuilder<UserAssignedPlan> builder)
    {
        builder.HasIndex(p => p.UserId);

        builder.HasOne(p => p.FitnessPlanConfig)
            .WithMany(c => c.UserAssignedPlans)
            .HasForeignKey(p => p.PlanId);
    }
}
