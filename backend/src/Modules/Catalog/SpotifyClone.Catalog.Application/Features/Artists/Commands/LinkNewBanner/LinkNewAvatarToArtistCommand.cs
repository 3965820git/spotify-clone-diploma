using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.LinkNewBanner;

public sealed record LinkNewBannerToArtistCommand(
    Guid ArtistId,
    Guid ImageId,
    int ImageWidth,
    int ImageHeight,
    string ImageFileType,
    long ImageSizeInBytes)
    : ICatalogPersistentCommand<LinkNewBannerToArtistCommandResult>;
