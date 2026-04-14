using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Genres.Commands.Rename;

public sealed record RenameGenreCommand(
    Guid GenreId,
    string Name)
    : ICatalogPersistentCommand<RenameGenreCommandResult>;
