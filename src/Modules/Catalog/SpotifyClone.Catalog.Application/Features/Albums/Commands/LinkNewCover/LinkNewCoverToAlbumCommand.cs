using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.LinkNewCover;

public sealed record LinkNewCoverToAlbumCommand(
    Guid AlbumId,
    Guid ImageId,
    int ImageWidth,
    int ImageHeight,
    string ImageFileType,
    long ImageSizeInBytes)
    : ICatalogPersistentCommand<LinkNewCoverToAlbumCommandResult>;
