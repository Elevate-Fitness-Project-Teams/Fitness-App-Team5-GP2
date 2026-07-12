using Fitness.UserProfile.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.UserProfile.Extensions;

public static class WebApplicationExtension
{
    public static  WebApplication ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        UserProfileDbContext? dbContext = scope.ServiceProvider.GetRequiredService<UserProfileDbContext>();
        dbContext.Database.Migrate();

        return app;
    }
}
