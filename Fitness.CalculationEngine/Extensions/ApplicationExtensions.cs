using FluentValidation;

namespace Fitness.CalculationEngine.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationDependancies(this IServiceCollection services)
    {
        //Add Mediatr
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        //Add Fluent Validation
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        return services;
    }
}
