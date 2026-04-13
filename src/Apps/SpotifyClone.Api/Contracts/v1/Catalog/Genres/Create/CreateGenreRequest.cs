namespace SpotifyClone.Api.Contracts.v1.Catalog.Genres.Create;

public sealed record CreateGenreRequest
{
    public required string Name { get; init; }
}
