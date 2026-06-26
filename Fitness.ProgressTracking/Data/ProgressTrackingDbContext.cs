using Fitness.ProgressTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitness.ProgressTracking.Data;

public class ProgressTrackingDbContext : DbContext
{
    public ProgressTrackingDbContext(DbContextOptions<ProgressTrackingDbContext> options)
        : base(options)
    {
    }

    public DbSet<WorkoutLog> WorkoutLogs => Set<WorkoutLog>();
    public DbSet<WorkoutLogExercise> WorkoutLogExercises => Set<WorkoutLogExercise>();
    public DbSet<WeightHistory> WeightHistories => Set<WeightHistory>();
    public DbSet<BodyMeasurement> BodyMeasurements => Set<BodyMeasurement>();
    public DbSet<Achievement> Achievements => Set<Achievement>();
    public DbSet<UserAchievement> UserAchievements => Set<UserAchievement>();
    public DbSet<Streak> Streaks => Set<Streak>();
    public DbSet<UserStatistics> UserStatistics => Set<UserStatistics>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProgressTrackingDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
