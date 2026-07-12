using Fitness.Nutrition.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Nutrition.Infrastructure.Persistence.Configurations;

public class NutritionFactsConfiguration : IEntityTypeConfiguration<NutritionFacts>
{
    public void Configure(EntityTypeBuilder<NutritionFacts> builder)
    {
        builder
        .HasOne(nf => nf.Meal)
        .WithOne(m => m.NutritionFacts)
        .HasForeignKey<NutritionFacts>(nf => nf.MealId)
        .OnDelete(DeleteBehavior.NoAction);
    }
}
