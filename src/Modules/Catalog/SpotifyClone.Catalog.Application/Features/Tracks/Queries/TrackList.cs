namespace SpotifyClone.Catalog.Application.Features.Tracks.Queries;

public sealed record TrackList(
    IEnumerable<TrackSummary> Tracks);
