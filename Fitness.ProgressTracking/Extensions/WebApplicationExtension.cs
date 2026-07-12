using Fitness.ProgressTracking.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fitness.ProgressTracking.Extensions;

public static class WebApplicationExtension
{
    public static  WebApplication ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        ProgressTrackingDbContext? dbContext = scope.ServiceProvider.GetRequiredService<ProgressTrackingDbContext>();
        dbContext.Database.Migrate();

        return app;
    }
}
