using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.DomainExceptions;

internal sealed class NullTestDomainException : DomainExceptionBase
{
    public NullTestDomainException() : base(null!)
    {
    }
}
