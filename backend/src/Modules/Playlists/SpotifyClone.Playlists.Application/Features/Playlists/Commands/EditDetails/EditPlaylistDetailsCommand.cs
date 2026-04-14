using SpotifyClone.Playlists.Application.Abstractions;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.EditDetails;

public sealed record EditPlaylistDetailsCommand(
    Guid PlaylistId,
    string Name,
    string? Description,
    bool IsPublic)
    : IPlaylistsPersistentCommand<EditPlaylistDetailsCommandResult>;
