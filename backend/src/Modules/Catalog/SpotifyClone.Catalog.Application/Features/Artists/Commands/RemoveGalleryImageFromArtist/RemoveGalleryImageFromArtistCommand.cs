using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.RemoveGalleryImageFromArtist;

public sealed record RemoveGalleryImageFromArtistCommand(
    Guid ArtistId,
    Guid ImageId)
    : ICatalogPersistentCommand<RemoveGalleryImageFromArtistCommandResult>;
