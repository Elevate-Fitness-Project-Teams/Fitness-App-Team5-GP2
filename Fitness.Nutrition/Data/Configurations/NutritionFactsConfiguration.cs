using Fitness.Nutrition.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Nutrition.Data.Configurations;

public class NutritionFactsConfiguration : IEntityTypeConfiguration<NutritionFacts>
{
    public void Configure(EntityTypeBuilder<NutritionFacts> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Calories)
            .IsRequired();

        builder.Property(n => n.Protein)
            .IsRequired();

        builder.Property(n => n.Carbs)
            .IsRequired();

        builder.Property(n => n.Fats)
            .IsRequired();

        builder.Property(n => n.Fiber)
            .IsRequired();
    }
}
