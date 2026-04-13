using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Catalog.Domain.Exceptions;

public abstract class CatalogDomainExceptionBase : DomainExceptionBase
{
    protected CatalogDomainExceptionBase(string message)
        : base(message)
    {
    }
}
