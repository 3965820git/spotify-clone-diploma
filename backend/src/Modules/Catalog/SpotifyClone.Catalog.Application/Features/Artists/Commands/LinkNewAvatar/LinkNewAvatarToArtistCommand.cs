using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.LinkNewAvatar;

public sealed record LinkNewAvatarToArtistCommand(
    Guid ArtistId,
    Guid ImageId,
    int ImageWidth,
    int ImageHeight,
    string ImageFileType,
    long ImageSizeInBytes)
    : ICatalogPersistentCommand<LinkNewAvatarToArtistCommandResult>;
