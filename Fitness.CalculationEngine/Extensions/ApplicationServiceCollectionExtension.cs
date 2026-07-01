using Fitness.CalculationEngine.Domain.Services;
using Fitness.CalculationEngine.Shared.Interfaces;
using Fitness.CalculationEngine.Shared.Services;

namespace Fitness.CalculationEngine.Extensions;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection RegisterApplicationDependancies(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser,CurrentUser>();
        services.AddScoped<CalculationService>();
        return services;
    }
}
