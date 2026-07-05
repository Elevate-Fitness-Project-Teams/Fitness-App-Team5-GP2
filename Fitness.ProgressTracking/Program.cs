using BuildingBlocks.Middleware;
using Fitness.ProgressTracking.Extensions;
using Fitness.ProgressTracking.Infrastructure.Persistence.Contexts;
using Fitness.ProgressTracking.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Fitness.ProgressTracking;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        //builder.Services.AddOpenApi();
        builder.Services.AddDbContext<ProgressTrackingDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.RegisterApplicationDependancies();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            //app.MapOpenApi().AllowAnonymous();
            app.MapScalarApiReference(options =>
            {
                options
                    .WithTitle("Fitness Progress Tracking")
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
            endpoint.MapEndpoint(globalGroup);
        app.ApplyDatabaseMigrations();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
