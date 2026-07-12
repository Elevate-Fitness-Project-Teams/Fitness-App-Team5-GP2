using BuildingBlocks.Extensions;
using Fitness.Workout.Repositories;

namespace Fitness.Workout.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationDependancies(this IServiceCollection services)
    {
        services.AddApplicationDependancies<IWorkoutMarker>();
        services.AddScoped<IWorkoutRepository, WorkoutRepository>();        
        return services;
    }
}
