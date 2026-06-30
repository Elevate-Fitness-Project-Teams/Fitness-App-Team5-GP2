using Fitness.CalculationEngine.Infrastructure.Persistence.DbContexts;
using Fitness.CalculationEngine.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fitness.CalculationEngine.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtenstion
{
    public static IServiceCollection AddInfrastructureDependancies(this IServiceCollection services ,IConfiguration configuration)
    {
        services.AddDbContext<CalculationEngineDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped(typeof(Repository<>));
        return services;
    }
}
