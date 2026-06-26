using BuildingBlocks.Extensions;

namespace Fitness.CalculationEngine.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationDependancies(this IServiceCollection services)
    {
        services.AddApplicationDependancies<ICalculationEngineMarker>();
        return services;
    }
}
