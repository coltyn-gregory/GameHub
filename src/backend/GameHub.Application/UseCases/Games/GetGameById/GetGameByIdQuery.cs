using MediatR;

namespace GameHub.Application.UseCases.Games.GetGameById;

public sealed record GetGameByIdQuery(string Id)
    : IRequest<GameReadModel?>;
