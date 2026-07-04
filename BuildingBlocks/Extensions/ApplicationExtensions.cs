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

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
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
        builder.Services.AddAuthorization();

        return services;
    }
}
