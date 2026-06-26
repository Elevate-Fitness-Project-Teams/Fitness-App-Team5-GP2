using Fitness.CalculationEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.CalculationEngine.Data.Configurations;

public class UserAssignedPlanConfiguration : IEntityTypeConfiguration<UserAssignedPlan>
{
    public void Configure(EntityTypeBuilder<UserAssignedPlan> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.UserId)
            .IsRequired();

        builder.HasIndex(p => p.UserId);

        builder.Property(p => p.PlanId)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.AssignedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(p => p.IsActive)
            .HasDefaultValue(true);

        builder.HasOne(p => p.FitnessPlanConfig)
            .WithMany(c => c.UserAssignedPlans)
            .HasForeignKey(p => p.PlanId);
    }
}
