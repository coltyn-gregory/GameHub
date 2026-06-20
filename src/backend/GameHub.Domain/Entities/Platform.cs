using GameHub.Domain.ValueObjects.Platform;

namespace GameHub.Domain.Entities;

public sealed class Platform : Entity
{
    public PlatformId Id { get; } = null!;
    public Name Name { get; private set; }

    // Required by EF Core
    private Platform()
    { }

    public static Platform? Create(
        PlatformId id,
        Name name)
    {
        // feat: add additional business rules in smart constructor

        return new Platform(
            id,
            name);
    }

    private Platform(
        PlatformId id,
        Name name)
    {
        Id = id;
        Name = name;
    }
}