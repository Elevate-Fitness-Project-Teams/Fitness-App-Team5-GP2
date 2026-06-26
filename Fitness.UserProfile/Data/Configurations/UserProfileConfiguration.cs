using Fitness.UserProfile.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.UserProfile.Data.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.ProfilePictureUrl)
            .HasMaxLength(500);

        builder.Property(p => p.IsPremiumCached)
            .HasDefaultValue(false);

        builder.Property(p => p.MemberSince)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(p => p.UserSettings)
            .WithOne()
            .HasForeignKey<UserSettings>(s => s.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.NotificationSettings)
            .WithOne()
            .HasForeignKey<NotificationSettings>(s => s.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.PrivacySettings)
            .WithOne()
            .HasForeignKey<PrivacySettings>(s => s.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
