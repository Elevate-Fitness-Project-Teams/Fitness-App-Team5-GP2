using Fitness.Nutrition.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Nutrition.Data.Configurations;

public class MealPlanItemConfiguration : IEntityTypeConfiguration<MealPlanItem>
{
    public void Configure(EntityTypeBuilder<MealPlanItem> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.DayOfWeek)
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(i => i.MealTime)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(i => i.MealPlan)
            .WithMany(p => p.MealPlanItems)
            .HasForeignKey(i => i.MealPlanId);

        builder.HasOne(i => i.Meal)
            .WithMany(m => m.MealPlanItems)
            .HasForeignKey(i => i.MealId);
    }
}
