using SpotifyClone.Catalog.Domain.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Albums.Exceptions;

public sealed class InvalidTrackInAlbumDomainException(string message)
    : CatalogDomainExceptionBase(message);
