using Fitness.Nutrition.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Nutrition.Extensions;

public static class WebApplicationExtension
{
    public static  WebApplication ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        NutritionDbContext? dbContext = scope.ServiceProvider.GetRequiredService<NutritionDbContext>();
        dbContext.Database.Migrate();

        return app;
    }
}
