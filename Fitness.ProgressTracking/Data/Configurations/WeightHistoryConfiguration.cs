using Fitness.ProgressTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.ProgressTracking.Data.Configurations;

public class WeightHistoryConfiguration : IEntityTypeConfiguration<WeightHistory>
{
    public void Configure(EntityTypeBuilder<WeightHistory> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.UserId)
            .IsRequired();

        builder.HasIndex(w => w.UserId);

        builder.Property(w => w.Weight)
            .IsRequired();

        builder.Property(w => w.Date)
            .IsRequired();

        builder.Property(w => w.Notes)
            .HasMaxLength(500);
    }
}
