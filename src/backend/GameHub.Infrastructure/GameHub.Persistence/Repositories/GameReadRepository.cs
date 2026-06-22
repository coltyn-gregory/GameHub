using GameHub.Application.UseCases.Games;
using GameHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameHub.Persistence.Repositories;

public sealed class GameReadRepository(GameHubDbContext dbContext) : IGameReadRepository
{
    private sealed record GameProjection(
        string Id,
        string Title,
        string StudioId,
        List<string> PlatformIds);

    public Task<IReadOnlyCollection<GameReadModel>> GetAllAsync(
        CancellationToken cancellationToken = default)
        => QueryAsync(dbContext.Games, cancellationToken);

    public Task<IReadOnlyCollection<GameReadModel>> GetByPlatformAsync(
        string platformId,
        CancellationToken cancellationToken = default)
        => QueryAsync(
            dbContext.Games.Where(game => game.PlatformIds.Any(p => p.Value == platformId)),
            cancellationToken);

    private async Task<IReadOnlyCollection<GameReadModel>> QueryAsync(
        IQueryable<Game> source,
        CancellationToken cancellationToken)
    {
        List<GameProjection> games = await source
            .AsNoTracking()
            .Select(game => new GameProjection(
                game.Id.Value,
                game.Title.Value,
                game.StudioId.Value,
                game.PlatformIds.Select(platform => platform.Value).ToList()))
            .ToListAsync(cancellationToken);

        Dictionary<string, string> platformNames = await dbContext.Platforms
            .AsNoTracking()
            .ToDictionaryAsync(p => p.Id.Value, p => p.Name.Value, cancellationToken);

        Dictionary<string, string> studioNames = await dbContext.Studios
            .AsNoTracking()
            .ToDictionaryAsync(s => s.Id.Value, s => s.Name.Value, cancellationToken);

        return games
            .Select(game => new GameReadModel(
                game.Id,
                game.Title,
                new StudioReadModel(
                    game.StudioId,
                    studioNames.GetValueOrDefault(game.StudioId, string.Empty)),
                game.PlatformIds
                    .Select(id => new PlatformReadModel(
                        id,
                        platformNames.GetValueOrDefault(id, string.Empty)))
                    .ToList()))
            .ToList();
    }
}
