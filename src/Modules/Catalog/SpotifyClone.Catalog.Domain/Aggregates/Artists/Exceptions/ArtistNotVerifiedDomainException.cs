using SpotifyClone.Catalog.Domain.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Artists.Exceptions;

public sealed class ArtistNotVerifiedDomainException(string message)
    : CatalogDomainExceptionBase(message);
