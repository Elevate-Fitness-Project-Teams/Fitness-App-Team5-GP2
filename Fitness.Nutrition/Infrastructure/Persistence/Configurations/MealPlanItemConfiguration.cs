using Fitness.Nutrition.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Nutrition.Infrastructure.Persistence.Configurations;

public class MealPlanItemConfiguration : IEntityTypeConfiguration<MealPlanItem>
{
    public void Configure(EntityTypeBuilder<MealPlanItem> builder)
    {

        builder.HasOne(i => i.MealPlan)
            .WithMany(p => p.MealPlanItems)
            .HasForeignKey(i => i.MealPlanId);

        builder.HasOne(i => i.Meal)
            .WithMany(m => m.MealPlanItems)
            .HasForeignKey(i => i.MealId);
    }
}
