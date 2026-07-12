using Fitness.ProgressTracking.Infrastructure.Persistence.Contexts;
using Fitness.ProgressTracking.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fitness.ProgressTracking.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtenstion
{
    public static IServiceCollection AddInfrastructureDependancies(this IServiceCollection services ,IConfiguration configuration)
    {
        services.AddDbContext<ProgressTrackingDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped(typeof(Repository<>));
        return services;
    }
}
