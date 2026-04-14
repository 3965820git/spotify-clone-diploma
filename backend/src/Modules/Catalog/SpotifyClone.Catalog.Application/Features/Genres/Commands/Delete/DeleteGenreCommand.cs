using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Genres.Commands.Delete;

public sealed record DeleteGenreCommand(
    Guid GenreId)
    : ICatalogPersistentCommand<DeleteGenreCommandResult>;
