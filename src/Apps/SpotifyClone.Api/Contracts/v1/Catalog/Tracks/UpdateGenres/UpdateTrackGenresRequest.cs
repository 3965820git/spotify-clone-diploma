namespace SpotifyClone.Api.Contracts.v1.Catalog.Tracks.UpdateGenres;

public sealed record UpdateTrackGenresRequest
{
    public required IEnumerable<Guid> GenreIds { get; init; }
}
