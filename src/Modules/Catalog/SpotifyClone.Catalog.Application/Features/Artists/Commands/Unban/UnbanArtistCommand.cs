using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.Unban;

public sealed record UnbanArtistCommand(
    Guid ArtistId)
    : ICatalogPersistentCommand<UnbanArtistCommandResult>;
