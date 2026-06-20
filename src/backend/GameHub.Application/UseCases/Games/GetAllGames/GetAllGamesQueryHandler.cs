using MediatR;

namespace GameHub.Application.UseCases.Games.GetAllGames;

public sealed class GetAllGamesQueryHandler(IGameReadRepository repository)
    : IRequestHandler<GetAllGamesQuery, IReadOnlyCollection<GameReadModel>>
{
    public Task<IReadOnlyCollection<GameReadModel>> Handle(
        GetAllGamesQuery request,
        CancellationToken cancellationToken)
        => repository.GetAllAsync(cancellationToken);
}
