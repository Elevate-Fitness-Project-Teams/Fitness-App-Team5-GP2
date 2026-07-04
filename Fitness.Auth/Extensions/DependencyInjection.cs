using BuildingBlocks.Extensions;
using Fitness.Auth.Infrastructure.Extensions;
using MassTransit;

namespace Fitness.Auth.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationDependancies(this IServiceCollection services ,IConfiguration configuration)
    {
        services.AddApplicationDependancies<IAuthMarker>();

        services.AddInfrastructureDependancies(configuration);
        return services;
    }
}
