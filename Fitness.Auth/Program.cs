using BuildingBlocks.Middleware;
using Fitness.Auth.Data;
using Fitness.Auth.Extensions;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Fitness.Auth;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        //builder.Services.AddOpenApi();
        builder.Services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.RegisterApplicationDependancies();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            //app.MapOpenApi().AllowAnonymous();
            app.MapScalarApiReference(options =>
            {
                options
                    .WithTitle("Fitness Auth")
                    .WithTheme(ScalarTheme.Purple)
                    .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
            }).AllowAnonymous();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
