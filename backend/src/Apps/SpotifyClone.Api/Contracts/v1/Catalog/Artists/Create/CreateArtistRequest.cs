namespace SpotifyClone.Api.Contracts.v1.Catalog.Artists.Create;

public sealed record CreateArtistRequest
{
    public required string Name { get; init; }
}
