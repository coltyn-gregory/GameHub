using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameHub.Persistence;

public static class MigrationExtensions
{
    public static async Task ApplyMigrationsAsync(this IServiceProvider services)
    {
        using IServiceScope scope = services.CreateScope();

        GameHubDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<GameHubDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
