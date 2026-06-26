using Fitness.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Auth.Data.Configurations;

public class OtpCodeConfiguration : IEntityTypeConfiguration<OtpCode>
{
    public void Configure(EntityTypeBuilder<OtpCode> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasIndex(o => o.Email);

        builder.Property(o => o.Code)
            .HasMaxLength(6)
            .IsRequired();

        builder.Property(o => o.ExpiresAt)
            .IsRequired();

        builder.Property(o => o.IsUsed)
            .HasDefaultValue(false);
    }
}
