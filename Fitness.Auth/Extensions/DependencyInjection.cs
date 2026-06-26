using BuildingBlocks.Extensions;

namespace Fitness.Auth.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationDependancies(this IServiceCollection services)
    {
        services.AddApplicationDependancies<IAuthMarker>();
        return services;
    }
}
