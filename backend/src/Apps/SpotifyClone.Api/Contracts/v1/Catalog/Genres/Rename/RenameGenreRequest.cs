namespace SpotifyClone.Api.Contracts.v1.Catalog.Genres.Rename;

public sealed record RenameGenreRequest
{
    public required string Name { get; init; }
}
