using SpotifyClone.Playlists.Application.Abstractions;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.RemoveTrackFromPlaylist;

public sealed record RemoveTrackFromPlaylistCommand(
    Guid PlaylistId,
    Guid TrackId)
    : IPlaylistsPersistentCommand<RemoveTrackFromPlaylistCommandResult>;
