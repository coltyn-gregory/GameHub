namespace GameHub.API.Models;

public sealed record UpdateGameRequest(
    string Title,
    string StudioId,
    IReadOnlyCollection<string> PlatformIds);
