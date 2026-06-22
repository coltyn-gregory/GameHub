using MediatR;

namespace GameHub.Application.UseCases.Games.GetGamesByPlatform;

public sealed record GetGamesByPlatformQuery(string PlatformId)
    : IRequest<IReadOnlyCollection<GameReadModel>>;
