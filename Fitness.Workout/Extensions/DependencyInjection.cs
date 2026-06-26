using BuildingBlocks.Extensions;

namespace Fitness.Workout.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationDependancies(this IServiceCollection services)
    {
        services.AddApplicationDependancies<IWorkoutMarker>();
        return services;
    }
}
