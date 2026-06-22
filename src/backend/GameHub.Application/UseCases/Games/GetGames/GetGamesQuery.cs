using MediatR;

namespace GameHub.Application.UseCases.Games.GetGames;

public sealed record GetGamesQuery(string? PlatformId, string? StudioId)
    : IRequest<IReadOnlyCollection<GameReadModel>>;
