using BuildingBlocks.Extensions;

namespace Fitness.UserProfile.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationDependancies(this IServiceCollection services)
    {
        services.AddApplicationDependancies<IUserProfileMarker>();
        return services;
    }
}
