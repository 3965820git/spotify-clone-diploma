using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Tests.Behaviors.TestExceptions;

internal sealed class TestDomainException : DomainExceptionBase
{
    public TestDomainException(string message) : base(message)
    {
    }
}
