using SpotifyClone.Catalog.Domain.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Albums.Exceptions;

public sealed class CannotPublishAlbumDomainException(string message)
    : CatalogDomainExceptionBase(message);
