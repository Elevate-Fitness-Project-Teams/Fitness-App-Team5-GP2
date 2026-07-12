using Fitness.Nutrition.Infrastructure.Integrations.FCEService;
using Fitness.Nutrition.Infrastructure.Persistence.Contexts;
using Fitness.Nutrition.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Nutrition.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtenstion
{
    public static IServiceCollection AddInfrastructureDependancies(this IServiceCollection services ,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<NutritionDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped(typeof(Repository<>));

        services.AddSingleton<FceGrpcClient>();
        return services;
    }
}
