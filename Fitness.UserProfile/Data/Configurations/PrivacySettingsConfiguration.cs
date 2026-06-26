using Fitness.UserProfile.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.UserProfile.Data.Configurations;

public class PrivacySettingsConfiguration : IEntityTypeConfiguration<PrivacySettings>
{
    public void Configure(EntityTypeBuilder<PrivacySettings> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.ProfileVisibility)
            .HasMaxLength(20)
            .HasDefaultValue("private");

        builder.Property(s => s.ShowProgressToFriends)
            .HasDefaultValue(false);

        builder.Property(s => s.AllowDataSharing)
            .HasDefaultValue(false);
    }
}
