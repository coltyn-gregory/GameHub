using GameHub.Domain.ValueObjects.Studio;

namespace GameHub.Domain.Entities;

public sealed class Studio : Entity
{
    public StudioId Id { get; }
    public Name Name { get; private set; }

    // Required by EF Core
    private Studio()
    { }

    public static Studio? Create(
        StudioId id,
        Name name)
    {
        // feat: add additional business rules in smart constructor

        return new Studio(
            id,
            name);
    }
    private Studio(
        StudioId id,
        Name name)
    {
        Id = id;
        Name = name;
    }
}