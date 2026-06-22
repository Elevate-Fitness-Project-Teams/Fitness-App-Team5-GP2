using BuildingBlocks;
using BuildingBlocks.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationDependancies<TServiceMarker>(this IServiceCollection services)
    {
        //Add Behaviour 
        services.AddTransient(typeof(IPipelineBehavior<,>),
                            typeof(ValidationBehaviour<,>));
        //Add Mediatr
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(TServiceMarker).Assembly);
        });

        //Add Fluent Validation
        services.AddValidatorsFromAssembly(typeof(TServiceMarker).Assembly);

        return services;
    }
}
