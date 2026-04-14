using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.Ban;

public sealed record BanArtistCommand(
    Guid ArtistId)
    : ICatalogPersistentCommand<BanArtistCommandResult>;
