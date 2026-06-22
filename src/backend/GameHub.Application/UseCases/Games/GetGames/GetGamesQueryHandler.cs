using MediatR;

namespace GameHub.Application.UseCases.Games.GetGames;

public sealed class GetGamesQueryHandler(IGameReadRepository repository)
    : IRequestHandler<GetGamesQuery, IReadOnlyCollection<GameReadModel>>
{
    public Task<IReadOnlyCollection<GameReadModel>> Handle(
        GetGamesQuery request,
        CancellationToken cancellationToken)
        => repository.GetAsync(request.PlatformId, request.StudioId, cancellationToken);
}
