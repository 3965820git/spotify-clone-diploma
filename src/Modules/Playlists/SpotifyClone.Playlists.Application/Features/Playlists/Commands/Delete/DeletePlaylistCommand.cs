using SpotifyClone.Playlists.Application.Abstractions;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.Delete;

public sealed record DeletePlaylistCommand(
    Guid PlaylistId)
    : IPlaylistsPersistentCommand<DeletePlaylistCommandResult>;
