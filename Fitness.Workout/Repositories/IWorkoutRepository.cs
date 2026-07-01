using Fitness.Workout.Domain.Entities;

namespace Fitness.Workout.Repositories;

public interface IWorkoutRepository
{
    Task<List<Domain.Entities.Workout>> GetAllAsync(
        string? category,
        string? difficulty,
        string? search,
        int? duration,
        int page,
        int pageSize,
        CancellationToken cancellationToken);

    Task<Domain.Entities.Workout?> GetByIdAsync(
        int workoutId,
        CancellationToken cancellationToken);

    Task<List<Domain.Entities.Workout>> GetByPlanIdAsync(
        string planId,
        CancellationToken cancellationToken);

    Task<List<Domain.Entities.Workout>> GetByCategoryAsync(
        string category,
        int page,
        int pageSize,
        CancellationToken cancellationToken);

    Task<bool> WorkoutExistsAsync(
        int workoutId,
        CancellationToken cancellationToken);

    Task<bool> PlanExistsAsync(
        string planId,
        CancellationToken cancellationToken);

    Task<List<WorkoutPlan>> GetAllPlansAsync(
        CancellationToken cancellationToken);

    Task<WorkoutPlan?> GetPlanByIdAsync(
        string planId,
        CancellationToken cancellationToken);

    Task<List<Exercise>> GetAllExercisesAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken);

    Task<Exercise?> GetExerciseByIdAsync(
        int exerciseId,
        CancellationToken cancellationToken);

    Task<WorkoutSession> CreateSessionAsync(
        WorkoutSession session,
        CancellationToken cancellationToken);
}
