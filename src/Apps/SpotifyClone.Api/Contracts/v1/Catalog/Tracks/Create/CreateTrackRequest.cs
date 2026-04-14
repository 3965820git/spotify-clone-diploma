namespace SpotifyClone.Api.Contracts.v1.Catalog.Tracks.Create;

public sealed record CreateTrackRequest
{
    public required string Title { get; init; }
    public required bool ContainsExplicitContent { get; init; }
    public required Guid AlbumId { get; init; }
    public required IEnumerable<Guid> MainArtists { get; init; }
    public IEnumerable<Guid> FeaturedArtists { get; init; } = [];
    public required IEnumerable<Guid> Genres { get; init; }
    public required IEnumerable<Guid> Moods { get; init; }
}
