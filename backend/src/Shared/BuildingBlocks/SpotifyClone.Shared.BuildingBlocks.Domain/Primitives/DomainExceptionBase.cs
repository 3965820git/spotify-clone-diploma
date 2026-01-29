namespace SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

public abstract class DomainExceptionBase : Exception
{
    protected DomainExceptionBase(string message)
        : base(message)
        => ArgumentException.ThrowIfNullOrWhiteSpace(message);
}
