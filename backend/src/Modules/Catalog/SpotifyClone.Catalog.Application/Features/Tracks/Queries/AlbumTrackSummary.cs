namespace SpotifyClone.Catalog.Application.Features.Tracks.Queries;

public sealed record AlbumTrackSummary(
    Guid Id,
    string Title,
    bool ContainsExplicitContent,
    string Status,
    TimeSpan? Duration,
    int Position);
