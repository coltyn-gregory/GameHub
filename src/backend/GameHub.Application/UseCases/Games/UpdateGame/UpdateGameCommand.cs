using MediatR;

namespace GameHub.Application.UseCases.Games.UpdateGame;

public sealed record UpdateGameCommand(
    string Id,
    string Title,
    string StudioId,
    IReadOnlyCollection<string> PlatformIds)
    : IRequest<bool>;
