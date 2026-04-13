namespace SpotifyClone.Api.Contracts.v1.Catalog.Moods.Rename;

public sealed record RenameMoodRequest
{
    public required string Name { get; init; }
}
