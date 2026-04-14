namespace SpotifyClone.Api.Contracts.v1.Catalog.Moods.Create;

public sealed record CreateMoodRequest
{
    public required string Name { get; init; }
}
