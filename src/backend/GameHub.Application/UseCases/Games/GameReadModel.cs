namespace GameHub.Application.UseCases.Games;

public sealed record GameReadModel(
    Guid Id,
    string Title,
    StudioReadModel Studio,
    IReadOnlyCollection<PlatformReadModel> Platforms);
