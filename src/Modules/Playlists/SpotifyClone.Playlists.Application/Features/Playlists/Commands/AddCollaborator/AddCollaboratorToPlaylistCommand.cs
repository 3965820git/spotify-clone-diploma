using SpotifyClone.Playlists.Application.Abstractions;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.AddCollaborator;

public sealed record AddCollaboratorToPlaylistCommand(
    Guid PlaylistId,
    Guid CollaboratorId)
    : IPlaylistsPersistentCommand<AddCollaboratorToPlaylistCommandResult>;
