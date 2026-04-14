using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.Verify;

public sealed record VerifyArtistCommand(
    Guid ArtistId)
    : ICatalogPersistentCommand<VerifyArtistCommandResult>;
