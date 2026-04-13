using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.AddGalleryImage;

public sealed record AddGalleryImageToArtistCommand(
    Guid ArtistId,
    Guid ImageId,
    int ImageWidth,
    int ImageHeight,
    string ImageFileType,
    long ImageSizeInBytes)
    : ICatalogPersistentCommand<AddGalleryImageToArtistCommandResult>;
