namespace GameHub.Application.UseCases.Games;

public interface IGameReadRepository
{
    Task<IReadOnlyCollection<GameReadModel>> GetAsync(
        string? platformId,
        string? studioId,
        CancellationToken cancellationToken = default);

    Task<GameReadModel?> GetByIdAsync(
        string id,
        CancellationToken cancellationToken = default);
}
