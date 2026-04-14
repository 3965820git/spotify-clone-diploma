using SpotifyClone.Catalog.Domain.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Artists.Exceptions;

public sealed class CannotVerifyArtistDomainException(string message)
    : CatalogDomainExceptionBase(message);
