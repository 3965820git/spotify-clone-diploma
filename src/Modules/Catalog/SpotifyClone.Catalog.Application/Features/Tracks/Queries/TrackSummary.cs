using SpotifyClone.Catalog.Application.Features.Artists.Queries;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Queries;

public sealed record TrackSummary(
    Guid Id,
    string Title,
    TimeSpan? Duration,
    DateTimeOffset? ReleaseDate,
    bool ContainsExplicitContent,
    string Status,
    Guid? AudioFileId,
    Guid? AlbumId,
    IEnumerable<ArtistSummary> MainArtists,
    IEnumerable<ArtistSummary> FeaturedArtists);
