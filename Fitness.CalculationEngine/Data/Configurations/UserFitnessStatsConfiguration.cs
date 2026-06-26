using Fitness.CalculationEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.CalculationEngine.Data.Configurations;

public class UserFitnessStatsConfiguration : IEntityTypeConfiguration<UserFitnessStats>
{
    public void Configure(EntityTypeBuilder<UserFitnessStats> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.UserId)
            .IsRequired();

        builder.HasIndex(s => s.UserId);

        builder.Property(s => s.Weight)
            .IsRequired();

        builder.Property(s => s.Height)
            .IsRequired();

        builder.Property(s => s.Age)
            .IsRequired();

        builder.Property(s => s.Gender)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(s => s.Goal)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(s => s.ActivityLevel)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(s => s.RecordedAt)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
