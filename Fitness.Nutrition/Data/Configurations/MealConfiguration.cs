using Fitness.Nutrition.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Nutrition.Data.Configurations;

public class MealConfiguration : IEntityTypeConfiguration<Meal>
{
    public void Configure(EntityTypeBuilder<Meal> builder)
    {
        builder.HasKey(m => m.MealId);

        builder.Property(m => m.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(m => m.Type)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(m => m.Calories)
            .IsRequired();

        builder.Property(m => m.Protein)
            .IsRequired();

        builder.Property(m => m.Carbs)
            .IsRequired();

        builder.Property(m => m.Fats)
            .IsRequired();

        builder.Property(m => m.PrepTimeInMinutes)
            .IsRequired();

        builder.Property(m => m.Difficulty)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(m => m.ImageUrl)
            .HasMaxLength(500);

        builder.Property(m => m.IngredientsJson)
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        builder.Property(m => m.InstructionsJson)
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        builder.Property(m => m.VariationsJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(m => m.AllergensJson)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(m => m.TagsJson)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasOne(m => m.NutritionFacts)
            .WithOne(n => n.Meal)
            .HasForeignKey<NutritionFacts>(n => n.Id);
    }
}
