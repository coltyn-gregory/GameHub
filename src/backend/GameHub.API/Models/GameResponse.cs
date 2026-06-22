namespace GameHub.API.Models;

public sealed record GameResponse(
    string Id,
    string Title,
    StudioResponse Studio,
    IReadOnlyCollection<PlatformResponse> Platforms);
