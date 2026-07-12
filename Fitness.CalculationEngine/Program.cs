using BuildingBlocks.Middleware;
using Fitness.CalculationEngine.Extensions;
using Fitness.CalculationEngine.Infrastructure.Integration.GRPC;
using Fitness.CalculationEngine.Infrastructure.Persistence.DbContexts;
using Fitness.CalculationEngine.Shared.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Fitness.CalculationEngine;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var isRunningInContainer = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
        builder.WebHost.ConfigureKestrel(options =>
        {
            if (isRunningInContainer)
            {
                // REST/Swagger/Scalar traffic — HTTP/1.1 on 8080
                options.ListenAnyIP(8080, o => o.Protocols = HttpProtocols.Http1);

                // gRPC traffic — HTTP/2 cleartext (h2c) on 8081
                options.ListenAnyIP(8081, o => o.Protocols = HttpProtocols.Http2);
            }
            else
            {
                // local `dotnet run` / VS debug — keep your existing dev ports
                options.ListenAnyIP(5264, o => o.Protocols = HttpProtocols.Http1AndHttp2);
                options.ListenAnyIP(7061, o => o.Protocols = HttpProtocols.Http1AndHttp2);
            }
        });
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
        app.ApplyDatabaseMigrations();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapGrpcService<FceGrpcService>();
        app.MapControllers();
        app.Run();
    }
}
