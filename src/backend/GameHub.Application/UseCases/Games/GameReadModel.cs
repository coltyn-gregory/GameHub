namespace GameHub.Application.UseCases.Games;

public sealed record GameReadModel(
    string Id,
    string Title,
    StudioReadModel Studio,
    IReadOnlyCollection<PlatformReadModel> Platforms);
