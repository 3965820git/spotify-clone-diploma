using SpotifyClone.Catalog.Domain.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Artists.Exceptions;

public sealed class InvalidArtistStatusDomainException(string message)
    : CatalogDomainExceptionBase(message);
