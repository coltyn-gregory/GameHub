using MediatR;

namespace GameHub.Application.UseCases.Games.GetGamesByPlatform;

public sealed class GetGamesByPlatformQueryHandler(IGameReadRepository repository)
    : IRequestHandler<GetGamesByPlatformQuery, IReadOnlyCollection<GameReadModel>>
{
    public Task<IReadOnlyCollection<GameReadModel>> Handle(
        GetGamesByPlatformQuery request,
        CancellationToken cancellationToken)
        => repository.GetByPlatformAsync(request.PlatformId, cancellationToken);
}
