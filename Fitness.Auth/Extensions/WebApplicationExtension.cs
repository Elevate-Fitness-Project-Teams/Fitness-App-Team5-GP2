using Fitness.Auth.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Auth.Extensions;

public static class WebApplicationExtension
{
    public static  WebApplication ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        AuthDbContext? dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        dbContext.Database.Migrate();

        return app;
    }
}
