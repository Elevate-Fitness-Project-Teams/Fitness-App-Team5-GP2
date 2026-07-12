using BuildingBlocks.Extensions;
using Fitness.Nutrition.Infrastructure.Extensions;

namespace Fitness.Nutrition.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationDependancies(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddApplicationDependancies<INutritionMarker>();
        services.AddInfrastructureDependancies(configuration);
        return services;
    }
}
