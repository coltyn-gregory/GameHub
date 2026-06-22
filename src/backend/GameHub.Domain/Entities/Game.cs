using GameHub.Domain.ValueObjects.Game;
using GameHub.Domain.ValueObjects.Platform;
using GameHub.Domain.ValueObjects.Studio;

namespace GameHub.Domain.Entities;

public sealed class Game : Entity
{
    public GameId Id { get; }
    public Title Title { get; private set; }
    public StudioId StudioId { get; private set; }
    public IReadOnlyCollection<PlatformId> PlatformIds { get; private set; } = new List<PlatformId>();

    // Required by EF Core
    private Game()
    { }

    public static Game? Create(
        GameId id,
        Title title,
        StudioId studioId,
        IEnumerable<PlatformId> platformIds)
    {
        // feat: add additional business rules in smart constructor

        return new Game(
            id,
            title,
            studioId,
            platformIds);
    }

    private Game(
        GameId id,
        Title title,
        StudioId studioId,
        IEnumerable<PlatformId> platformIds)
    {
        Id = id;
        Title = title;
        StudioId = studioId;
        PlatformIds = platformIds.ToList();
    }

    public void Update(
        Title title,
        StudioId studioId,
        IEnumerable<PlatformId> platformIds)
    {
        Title = title;
        StudioId = studioId;
        PlatformIds = platformIds.ToList();
    }
}