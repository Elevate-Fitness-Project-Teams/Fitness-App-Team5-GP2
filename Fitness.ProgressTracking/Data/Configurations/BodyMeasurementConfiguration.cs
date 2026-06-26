using Fitness.ProgressTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.ProgressTracking.Data.Configurations;

public class BodyMeasurementConfiguration : IEntityTypeConfiguration<BodyMeasurement>
{
    public void Configure(EntityTypeBuilder<BodyMeasurement> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.UserId)
            .IsRequired();

        builder.HasIndex(m => m.UserId);

        builder.Property(m => m.RecordedAt)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
