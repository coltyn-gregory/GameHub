using GameHub.Application.UseCases.Games;
using GameHub.Domain.Entities;
using GameHub.Domain.ValueObjects.Game;
using Microsoft.EntityFrameworkCore;

namespace GameHub.Persistence.Repositories;

public sealed class GameRepository(GameHubDbContext dbContext) : IGameRepository
{
    public Task<Game?> GetByIdAsync(GameId id, CancellationToken cancellationToken = default)
        => dbContext.Games.FirstOrDefaultAsync(game => game.Id == id, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => dbContext.SaveChangesAsync(cancellationToken);
}
