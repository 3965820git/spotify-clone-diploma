using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.Unverify;

public sealed record UnverifyArtistCommand(
    Guid ArtistId)
    : ICatalogPersistentCommand<UnverifyArtistCommandResult>;
