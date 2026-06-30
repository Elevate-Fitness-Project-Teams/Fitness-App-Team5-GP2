using BuildingBlocks.Extensions;
using Fitness.UserProfile.Common;

namespace Fitness.UserProfile.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationDependancies(this IServiceCollection services)
    {
        services.AddApplicationDependancies<IUserProfileMarker>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();
        return services;
    }
}
