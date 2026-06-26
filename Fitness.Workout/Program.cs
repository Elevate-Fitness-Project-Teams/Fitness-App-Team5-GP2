using BuildingBlocks.Middleware;
using Fitness.Workout.Data;
using Fitness.Workout.Extensions;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Fitness.Workout;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        //builder.Services.AddOpenApi();
        builder.Services.AddDbContext<WorkoutDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.RegisterApplicationDependancies();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            //app.MapOpenApi().AllowAnonymous();
            app.MapScalarApiReference(options =>
            {
                options
                    .WithTitle("Fitness Workout")
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
