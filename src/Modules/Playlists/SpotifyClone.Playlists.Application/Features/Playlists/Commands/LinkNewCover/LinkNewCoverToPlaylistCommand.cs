using SpotifyClone.Playlists.Application.Abstractions;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.LinkNewCover;

public sealed record LinkNewCoverToPlaylistCommand(
    Guid PlaylistId,
    Guid ImageId,
    int ImageWidth,
    int ImageHeight,
    string ImageFileType,
    long ImageSizeInBytes)
    : IPlaylistsPersistentCommand<LinkNewCoverToPlaylistCommandResult>;
