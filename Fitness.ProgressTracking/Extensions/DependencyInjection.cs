using BuildingBlocks.Extensions;

namespace Fitness.ProgressTracking.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationDependancies(this IServiceCollection services)
    {
        services.AddApplicationDependancies<IProgressTrackingMarker>();
        return services;
    }
}
