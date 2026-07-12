using Fitness.Nutrition.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Nutrition.Infrastructure.Persistence.Configurations;

public class MealConfiguration : IEntityTypeConfiguration<Meal>
{
    public void Configure(EntityTypeBuilder<Meal> builder)
    {
        builder.HasOne(m => m.NutritionFacts)
            .WithOne(n => n.Meal)
            .HasForeignKey<NutritionFacts>(n => n.Id);
    }
}
