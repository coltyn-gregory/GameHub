namespace GameHub.Application.UseCases.Games;

public interface IGameReadRepository
{
    Task<IReadOnlyCollection<GameReadModel>> GetAllAsync(CancellationToken cancellationToken = default);
}
