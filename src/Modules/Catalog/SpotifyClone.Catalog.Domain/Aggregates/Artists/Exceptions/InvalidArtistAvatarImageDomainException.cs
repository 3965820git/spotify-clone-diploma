using SpotifyClone.Catalog.Domain.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Artists.Exceptions;

public sealed class InvalidArtistAvatarImageDomainException(string message)
    : CatalogDomainExceptionBase(message);
