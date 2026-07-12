using BuildingBlocks.Extensions;
using BuildingBlocks.Models;
using Fitness.UserProfile.Common;
using Fitness.UserProfile.Repositories;
using MassTransit;

namespace Fitness.UserProfile.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationDependancies(this IServiceCollection services ,IConfiguration configuration)
    {
        services.AddApplicationDependancies<IUserProfileMarker>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped(typeof(Repository<>));
        services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));
        services.AddMassTransit(x =>
        {
            // 1. Register consumers from the assembly
            x.AddConsumers(typeof(IUserProfileMarker).Assembly);

            x.UsingRabbitMq((context, cfg) =>
            {

                // 2. Configure RabbitMQ connection
                var rabbitMqSettings = configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>();
                var rabbitMqHost = rabbitMqSettings.Host ?? "localhost";
                var rabbitMqUser = rabbitMqSettings.UserName ?? "guest";
                var rabbitMqPass = rabbitMqSettings.Password ?? "guest";

                cfg.Host(rabbitMqHost, "/", hostConfigurator => {
                    hostConfigurator.Username(rabbitMqUser);
                    hostConfigurator.Password(rabbitMqPass);
                });

                // 3. Configure retry policy for resilience
                cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));

                // 4. Configure endpoints (this will auto-configure queues for your consumers)
                cfg.ConfigureEndpoints(context);
            });

            // Optional: Add Outbox Pattern for reliability (see section below)
            // x.AddEntityFrameworkOutbox<YourDbContext>();
        });
        return services;
    }
}
