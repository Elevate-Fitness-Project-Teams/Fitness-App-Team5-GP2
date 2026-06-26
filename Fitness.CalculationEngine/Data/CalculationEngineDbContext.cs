using Fitness.CalculationEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitness.CalculationEngine.Data;

public class CalculationEngineDbContext : DbContext
{
    public CalculationEngineDbContext(DbContextOptions<CalculationEngineDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserFitnessStats> UserFitnessStats => Set<UserFitnessStats>();
    public DbSet<CalculatedMetrics> CalculatedMetrics => Set<CalculatedMetrics>();
    public DbSet<FitnessPlanConfig> FitnessPlanConfigs => Set<FitnessPlanConfig>();
    public DbSet<UserAssignedPlan> UserAssignedPlans => Set<UserAssignedPlan>();
    public DbSet<UserPlanHistory> UserPlanHistories => Set<UserPlanHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CalculationEngineDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
