using Fitness.CalculationEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.CalculationEngine.Data.Configurations;

public class UserPlanHistoryConfiguration : IEntityTypeConfiguration<UserPlanHistory>
{
    public void Configure(EntityTypeBuilder<UserPlanHistory> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.UserId)
            .IsRequired();

        builder.HasIndex(h => h.UserId);

        builder.Property(h => h.PlanId)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(h => h.AssignedAt)
            .IsRequired();

        builder.Property(h => h.ReasonForChange)
            .HasMaxLength(255)
            .IsRequired();
    }
}
