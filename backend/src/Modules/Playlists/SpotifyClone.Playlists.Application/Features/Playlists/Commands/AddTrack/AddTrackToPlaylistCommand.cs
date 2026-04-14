using SpotifyClone.Playlists.Application.Abstractions;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.AddTrack;

public sealed record AddTrackToPlaylistCommand(
    Guid PlaylistId,
    Guid TrackId)
    : IPlaylistsPersistentCommand<AddTrackToPlaylistCommandResult>;
