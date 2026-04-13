namespace SpotifyClone.Api.Contracts.v1.Catalog.Artists.EditProfile;

public sealed record EditArtistProfileRequest
{
    public required string Name { get; init; }
    public required string? Bio { get; init; }
}
