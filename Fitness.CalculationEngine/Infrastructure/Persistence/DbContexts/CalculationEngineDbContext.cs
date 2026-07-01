using Fitness.CalculationEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitness.CalculationEngine.Infrastructure.Persistence.DbContexts;

public class CalculationEngineDbContext : DbContext
{
    public CalculationEngineDbContext(DbContextOptions<CalculationEngineDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserFitnessStat> UserFitnessStats {  get; set; }
    public DbSet<CalculatedMetrics> CalculatedMetrics { get; set; }
    public DbSet<FitnessPlanConfig> FitnessPlanConfigs { get; set; }
    public DbSet<UserAssignedPlan> UserAssignedPlans { get; set; }
    public DbSet<UserPlanHistory> UserPlanHistories{  get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CalculationEngineDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
