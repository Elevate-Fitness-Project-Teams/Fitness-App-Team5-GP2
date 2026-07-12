using BuildingBlocks.Models;
using Fitness.Auth.Domain.Entities;
using Fitness.Auth.Infrastructure.Persistence.DbContexts;
using Fitness.Auth.Shared.Models;
using Fitness.Auth.Shared.Repositories;
using Fitness.Auth.Shared.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Fitness.Auth.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtenstion
{
    public static IServiceCollection AddInfrastructureDependancies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
        services.AddScoped<TokenService>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                ClockSkew = TimeSpan.FromSeconds(30)
            };
        });
        services.AddAuthorization();
        services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        })
        .AddEntityFrameworkStores<AuthDbContext>()
        .AddDefaultTokenProviders();
        services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));
        services.AddMassTransit(x =>
        {

            // 1. Register consumers from the assembly
            x.AddConsumers(typeof(IAuthMarker).Assembly);

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
        services.AddScoped(typeof(Repository<>));

        return services;
    }
}
