using Fitness.Auth.Domain.Entities;
using Fitness.Auth.Infrastructure.Persistence.DbContexts;
using Fitness.Auth.Shared.Models;
using Fitness.Auth.Shared.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        }).AddEntityFrameworkStores<AuthDbContext>();
        services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMQ:Host"], "/", h =>
                {
                    h.Username(configuration["RabbitMQ:UserName"]);
                    h.Password(configuration["RabbitMQ:Password"]);
                });
            });
        });
        return services;
    }
}
