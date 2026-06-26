using Fitness.UserProfile.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.UserProfile.Data.Configurations;

public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
{
    public void Configure(EntityTypeBuilder<UserSettings> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Language)
            .HasMaxLength(10)
            .HasDefaultValue("en");

        builder.Property(s => s.Theme)
            .HasMaxLength(15)
            .HasDefaultValue("light");

        builder.Property(s => s.WeightUnit)
            .HasMaxLength(5)
            .HasDefaultValue("kg");

        builder.Property(s => s.HeightUnit)
            .HasMaxLength(5)
            .HasDefaultValue("cm");

        builder.Property(s => s.DistanceUnit)
            .HasMaxLength(5)
            .HasDefaultValue("km");
    }
}
