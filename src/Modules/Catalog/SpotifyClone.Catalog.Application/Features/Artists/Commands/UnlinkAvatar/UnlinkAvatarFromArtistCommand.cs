using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.UnlinkAvatar;

public sealed record UnlinkAvatarFromArtistCommand(
    Guid ArtistId)
    : ICatalogPersistentCommand<UnlinkAvatarFromArtistCommandResult>;
