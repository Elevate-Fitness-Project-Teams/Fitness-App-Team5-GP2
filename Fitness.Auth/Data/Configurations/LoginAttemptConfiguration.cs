using Fitness.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Auth.Data.Configurations;

public class LoginAttemptConfiguration : IEntityTypeConfiguration<LoginAttempt>
{
    public void Configure(EntityTypeBuilder<LoginAttempt> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(l => l.AttemptedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(l => l.IsSuccess)
            .IsRequired();

        builder.Property(l => l.IpAddress)
            .HasMaxLength(45)
            .IsRequired();
    }
}
