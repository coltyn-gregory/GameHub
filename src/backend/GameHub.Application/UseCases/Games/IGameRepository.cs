using GameHub.Domain.Entities;
using GameHub.Domain.ValueObjects.Game;

namespace GameHub.Application.UseCases.Games;

public interface IGameRepository
{
    Task<Game?> GetByIdAsync(GameId id, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
