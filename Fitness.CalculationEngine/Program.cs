using BuildingBlocks.Middleware;
using Fitness.CalculationEngine.Extensions;
using Fitness.CalculationEngine.Infrastructure.Persistence.DbContexts;
using Fitness.CalculationEngine.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Fitness.CalculationEngine;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        //builder.Services.AddOpenApi();

        builder.Services.RegisterApplicationDependancies(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            //app.MapOpenApi().AllowAnonymous();
            app.UseSwagger();
            app.UseSwaggerUI();

            // If you're using Scalar (third-party API reference UI)
            app.MapScalarApiReference(options =>
            {
                options
                    .WithTitle("Fitness Calculation Engine")
                    .WithTheme(ScalarTheme.Purple)
                    .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
            }).AllowAnonymous();
        }
        var globalGroup = app.MapGroup("");
        var endpointDefinitions = typeof(Program).Assembly
        .GetTypes()
        .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && !t.IsAbstract)
        .Select(Activator.CreateInstance)
        .Cast<IEndpoint>();

        foreach (var endpoint in endpointDefinitions)
        {
            endpoint.MapEndpoint(globalGroup);
        }
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
