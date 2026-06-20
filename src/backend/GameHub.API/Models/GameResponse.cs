namespace GameHub.API.Models;

public sealed record GameResponse(
    Guid Id,
    string Title,
    StudioResponse Studio,
    IReadOnlyCollection<PlatformResponse> Platforms);
