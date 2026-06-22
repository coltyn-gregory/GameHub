using GameHub.Application.UseCases.Games;
using GameHub.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameHub.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<GameHubDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("GameHub"),
                sql => sql.MigrationsAssembly(typeof(GameHubDbContext).Assembly.FullName)));

        services.AddScoped<IGameReadRepository, GameReadRepository>();
        services.AddScoped<IGameRepository, GameRepository>();

        return services;
    }
}
