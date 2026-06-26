using BuildingBlocks.Extensions;

namespace Fitness.Nutrition.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationDependancies(this IServiceCollection services)
    {
        services.AddApplicationDependancies<INutritionMarker>();
        return services;
    }
}
