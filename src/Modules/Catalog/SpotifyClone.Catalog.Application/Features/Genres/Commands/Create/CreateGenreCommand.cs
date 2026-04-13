using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Genres.Commands.Create;

public sealed record CreateGenreCommand(
    string Name)
    : ICatalogPersistentCommand<CreateGenreCommandResult>;
