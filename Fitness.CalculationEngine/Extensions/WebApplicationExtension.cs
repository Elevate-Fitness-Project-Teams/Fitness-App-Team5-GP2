using Fitness.CalculationEngine.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Fitness.CalculationEngine.Extensions;

public static class WebApplicationExtension
{
    public static  WebApplication ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        CalculationEngineDbContext? dbContext = scope.ServiceProvider.GetRequiredService<CalculationEngineDbContext>();
        dbContext.Database.Migrate();

        return app;
    }
}
