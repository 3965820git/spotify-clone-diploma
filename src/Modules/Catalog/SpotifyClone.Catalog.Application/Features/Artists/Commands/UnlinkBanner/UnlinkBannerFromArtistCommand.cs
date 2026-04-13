using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.UnlinkBanner;

public sealed record UnlinkBannerFromArtistCommand(
    Guid ArtistId)
    : ICatalogPersistentCommand<UnlinkBannerFromArtistCommandResult>;
