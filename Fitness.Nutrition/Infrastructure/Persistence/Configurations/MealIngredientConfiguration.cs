using Fitness.Nutrition.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Nutrition.Infrastructure.Persistence.Configurations;

public class MealIngredientConfiguration : IEntityTypeConfiguration<MealIngredient>
{
    public void Configure(EntityTypeBuilder<MealIngredient> builder)
    {

        builder.HasOne(mi => mi.Meal)
            .WithMany(m => m.MealIngredients)
            .HasForeignKey(mi => mi.MealId);

        builder.HasOne(mi => mi.Ingredient)
            .WithMany(i => i.MealIngredients)
            .HasForeignKey(mi => mi.IngredientId);
    }
}
