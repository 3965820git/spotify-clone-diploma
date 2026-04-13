using SpotifyClone.Playlists.Application.Abstractions;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.RemoveCollaborator;

public sealed record RemoveCollaboratorFromPlaylistCommand(
    Guid PlaylistId,
    Guid CollaboratorId)
    : IPlaylistsPersistentCommand<RemoveCollaboratorFromPlaylistCommandResult>;
