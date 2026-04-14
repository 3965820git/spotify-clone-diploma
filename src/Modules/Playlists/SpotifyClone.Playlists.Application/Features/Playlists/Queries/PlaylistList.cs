namespace SpotifyClone.Playlists.Application.Features.Playlists.Queries;

public sealed record PlaylistList(
    IReadOnlyCollection<PlaylistSummary> Playlists);
