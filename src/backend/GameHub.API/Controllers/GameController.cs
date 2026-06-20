using GameHub.API.Models;
using GameHub.Application.UseCases.Games;
using GameHub.Application.UseCases.Games.GetAllGames;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace GameHub.API.Controllers;

[ApiController]
[Route("api/games")]
public sealed class GameController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyCollection<GameResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        IReadOnlyCollection<GameReadModel> games =
            await sender.Send(new GetAllGamesQuery(), cancellationToken);

        List<GameResponse> response = games
            .Select(game => new GameResponse(
                game.Id,
                game.Title,
                new StudioResponse(game.Studio.Id, game.Studio.Name),
                game.Platforms
                    .Select(platform => new PlatformResponse(platform.Id, platform.Name))
                    .ToList()))
            .ToList();

        return Ok(response);
    }
}
