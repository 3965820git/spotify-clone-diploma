using SpotifyClone.Catalog.Domain.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Artists.Exceptions;

public sealed class InvalidArtistBannerImageDomainException(string message)
    : CatalogDomainExceptionBase(message);
