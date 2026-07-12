using Fitness.Nutrition.Shared.Interfaces;
using Fitness.Nutrition.Shared.Services;

namespace Fitness.Nutrition.Extensions;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplicationDependancies(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser,CurrentUser>();
        return services;
    }
}
