using Fitness.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Auth.Infrastructure.Persistence.Configurations;

public class LoginAttemptConfiguration : IEntityTypeConfiguration<LoginAttempt>
{
    public void Configure(EntityTypeBuilder<LoginAttempt> builder)
    {
    }
}
