using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GameHub.Persistence;

// Used only by the EF Core tools (dotnet ef migrations/database) at design time.
// The connection string here is for tooling; runtime config comes from the API.
public sealed class GameHubDbContextFactory : IDesignTimeDbContextFactory<GameHubDbContext>
{
    public GameHubDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<GameHubDbContext>()
            .UseSqlServer("Server=localhost;Database=GameHub;Trusted_Connection=True;TrustServerCertificate=True")
            .Options;

        return new GameHubDbContext(options, TimeProvider.System);
    }
}
