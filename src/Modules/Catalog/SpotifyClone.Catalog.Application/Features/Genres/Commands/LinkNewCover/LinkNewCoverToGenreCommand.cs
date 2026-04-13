using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Genres.Commands.LinkNewCover;

public sealed record LinkNewCoverToGenreCommand(
    Guid GenreId,
    Guid ImageId,
    int ImageWidth,
    int ImageHeight,
    string ImageFileType,
    long ImageSizeInBytes)
    : ICatalogPersistentCommand<LinkNewCoverToGenreCommandResult>;
