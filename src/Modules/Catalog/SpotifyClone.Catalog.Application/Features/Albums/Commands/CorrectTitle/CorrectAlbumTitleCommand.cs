using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.CorrectTitle;

public sealed record CorrectAlbumTitleCommand(
    Guid AlbumId,
    string Title)
    : ICatalogPersistentCommand<CorrectAlbumTitleCommandResult>;
