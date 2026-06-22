using GameHub.API.Models;
using GameHub.Application.UseCases.Games;
using GameHub.Application.UseCases.Games.GetGameById;
using GameHub.Application.UseCases.Games.GetGames;
using GameHub.Application.UseCases.Games.UpdateGame;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace GameHub.API.Controllers;

[ApiController]
[Route("api/games")]
public sealed class GameController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyCollection<GameResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? platformId,
        [FromQuery] string? studioId,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<GameReadModel> games =
            await sender.Send(new GetGamesQuery(platformId, studioId), cancellationToken);

        List<GameResponse> response = games
            .Select(ToResponse)
            .ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<GameResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        string id,
        CancellationToken cancellationToken)
    {
        GameReadModel? game = await sender.Send(new GetGameByIdQuery(id), cancellationToken);

        return game is null ? NotFound() : Ok(ToResponse(game));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        string id,
        [FromBody] UpdateGameRequest request,
        CancellationToken cancellationToken)
    {
        bool updated = await sender.Send(
            new UpdateGameCommand(id, request.Title, request.StudioId, request.PlatformIds),
            cancellationToken);

        return updated ? NoContent() : NotFound();
    }

    private static GameResponse ToResponse(GameReadModel game)
        => new(
            game.Id,
            game.Title,
            new StudioResponse(game.Studio.Id, game.Studio.Name),
            game.Platforms
                .Select(platform => new PlatformResponse(platform.Id, platform.Name))
                .ToList());
}
