using Fitness.Workout.Data;
using Fitness.Workout.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Workout.Repositories;

public class WorkoutRepository(WorkoutDbContext context) : IWorkoutRepository
{
    public async Task<List<Domain.Entities.Workout>> GetAllAsync(
        string? category,
        string? difficulty,
        string? search,
        int? duration,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = context.Workouts.AsQueryable();

        if (category is not null)
            query = query.Where(w => w.Category == category);

        if (difficulty is not null)
            query = query.Where(w => w.Difficulty == difficulty);

        if (search is not null)
            query = query.Where(w => w.Name.Contains(search));

        if (duration is not null)
            query = query.Where(w => w.DurationInMinutes <= duration);

        return await query
            .OrderBy(w => w.WorkoutId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Domain.Entities.Workout?> GetByIdAsync(
        int workoutId,
        CancellationToken cancellationToken)
    {
        return await context.Workouts
            .Include(w => w.WorkoutExercises)
                .ThenInclude(we => we.Exercise)
            .FirstOrDefaultAsync(w => w.WorkoutId == workoutId, cancellationToken);
    }

    public async Task<List<Domain.Entities.Workout>> GetByPlanIdAsync(
        string planId,
        CancellationToken cancellationToken)
    {
        return await context.Workouts
            .Where(w => w.PlanId == planId)
            .OrderBy(w => w.WorkoutId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Domain.Entities.Workout>> GetByCategoryAsync(
        string category,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        return await context.Workouts
            .Where(w => w.Category == category.ToLower())
            .OrderBy(w => w.WorkoutId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> WorkoutExistsAsync(
        int workoutId,
        CancellationToken cancellationToken)
    {
        return await context.Workouts
            .AnyAsync(w => w.WorkoutId == workoutId, cancellationToken);
    }

    public async Task<bool> PlanExistsAsync(
        string planId,
        CancellationToken cancellationToken)
    {
        return await context.WorkoutPlans
            .AnyAsync(p => p.PlanId == planId, cancellationToken);
    }

    public async Task<List<WorkoutPlan>> GetAllPlansAsync(
        CancellationToken cancellationToken)
    {
        return await context.WorkoutPlans
            .ToListAsync(cancellationToken);
    }

    public async Task<WorkoutPlan?> GetPlanByIdAsync(
        string planId,
        CancellationToken cancellationToken)
    {
        return await context.WorkoutPlans
            .Include(p => p.Workouts)
            .FirstOrDefaultAsync(p => p.PlanId == planId, cancellationToken);
    }

    public async Task<List<Exercise>> GetAllExercisesAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        return await context.Exercises
            .OrderBy(e => e.ExerciseId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Exercise?> GetExerciseByIdAsync(
        int exerciseId,
        CancellationToken cancellationToken)
    {
        return await context.Exercises
            .FirstOrDefaultAsync(e => e.ExerciseId == exerciseId, cancellationToken);
    }

    public async Task<WorkoutSession> CreateSessionAsync(
        WorkoutSession session,
        CancellationToken cancellationToken)
    {
        context.WorkoutSessions.Add(session);
        await context.SaveChangesAsync(cancellationToken);
        return session;
    }
}
