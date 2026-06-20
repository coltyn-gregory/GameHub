using GameHub.Application.UseCases.Games;
using Microsoft.EntityFrameworkCore;

namespace GameHub.Persistence.Repositories;

public sealed class GameReadRepository(GameHubDbContext dbContext) : IGameReadRepository
{
    private sealed record GameProjection(
        Guid Id,
        string Title,
        Guid StudioId,
        List<Guid> PlatformIds);

    public async Task<IReadOnlyCollection<GameReadModel>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        List<GameProjection> games = await dbContext.Games
            .AsNoTracking()
            .Select(game => new GameProjection(
                game.Id.Value,
                game.Title.Value,
                game.StudioId.Value,
                game.PlatformIds.Select(platform => platform.Value).ToList()))
            .ToListAsync(cancellationToken);

        Dictionary<Guid, string> platformNames = await dbContext.Platforms
            .AsNoTracking()
            .ToDictionaryAsync(p => p.Id.Value, p => p.Name.Value, cancellationToken);

        Dictionary<Guid, string> studioNames = await dbContext.Studios
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
