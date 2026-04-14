using SpotifyClone.Playlists.Application.Abstractions;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.MoveTrack;

public sealed record MoveTrackInPlaylistCommand(
    Guid PlaylistId,
    Guid TrackId,
    int TargetPositionIndex)
    : IPlaylistsPersistentCommand<MoveTrackInPlaylistCommandResult>;
