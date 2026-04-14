using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Genres.Commands.UnlinkCover;

public sealed record UnlinkCoverFromGenreCommand(
    Guid GenreId)
    : ICatalogPersistentCommand<UnlinkCoverFromGenreCommandResult>;
