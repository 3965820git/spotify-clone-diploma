using SpotifyClone.Playlists.Application.Abstractions;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.Create;

public sealed record CreatePlaylistCommand
    : IPlaylistsPersistentCommand<CreatePlaylistCommandResult>;
