using BuildingBlocks.Extensions;
using Fitness.CalculationEngine.Infrastructure.Extensions;

namespace Fitness.CalculationEngine.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationDependancies(this IServiceCollection services ,IConfiguration configuration)
    {
        services.AddApplicationDependancies<ICalculationEngineMarker>();
        services.AddInfrastructureDependancies(configuration);
        return services;
    }
}
