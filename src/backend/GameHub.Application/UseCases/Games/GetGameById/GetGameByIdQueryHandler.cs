using MediatR;

namespace GameHub.Application.UseCases.Games.GetGameById;

public sealed class GetGameByIdQueryHandler(IGameReadRepository repository)
    : IRequestHandler<GetGameByIdQuery, GameReadModel?>
{
    public Task<GameReadModel?> Handle(
        GetGameByIdQuery request,
        CancellationToken cancellationToken)
        => repository.GetByIdAsync(request.Id, cancellationToken);
}
