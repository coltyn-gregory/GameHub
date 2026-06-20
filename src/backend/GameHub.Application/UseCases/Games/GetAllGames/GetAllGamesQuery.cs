using MediatR;

namespace GameHub.Application.UseCases.Games.GetAllGames;

public sealed record GetAllGamesQuery : IRequest<IReadOnlyCollection<GameReadModel>>;
