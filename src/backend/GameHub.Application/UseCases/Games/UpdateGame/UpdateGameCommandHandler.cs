using GameHub.Domain.Entities;
using GameHub.Domain.ValueObjects.Game;
using GameHub.Domain.ValueObjects.Platform;
using GameHub.Domain.ValueObjects.Studio;
using MediatR;

namespace GameHub.Application.UseCases.Games.UpdateGame;

public sealed class UpdateGameCommandHandler(IGameRepository repository)
    : IRequestHandler<UpdateGameCommand, bool>
{
    public async Task<bool> Handle(
        UpdateGameCommand request,
        CancellationToken cancellationToken)
    {
        Game? game = await repository.GetByIdAsync(new GameId(request.Id), cancellationToken);

        if (game is null)
        {
            return false;
        }

        game.Update(
            new Title(request.Title),
            new StudioId(request.StudioId),
            request.PlatformIds.Select(id => new PlatformId(id)));

        await repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}
