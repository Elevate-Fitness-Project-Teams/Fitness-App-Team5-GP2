using BuildingBlocks.Middleware;
using Fitness.UserProfile.Data;
using Fitness.UserProfile.Extensions;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Fitness.UserProfile;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        //builder.Services.AddOpenApi();
        builder.Services.AddDbContext<UserProfileDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.RegisterApplicationDependancies();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
           //app.MapOpenApi().AllowAnonymous();
            app.MapScalarApiReference(options =>
            {
                options
                    .WithTitle("Fitness User Profile")
                    .WithTheme(ScalarTheme.Purple)
                    .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
            }).AllowAnonymous();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseStaticFiles(); // serves uploaded profile pictures from wwwroot
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
