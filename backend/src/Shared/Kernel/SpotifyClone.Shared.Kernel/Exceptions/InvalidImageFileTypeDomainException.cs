using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.Kernel.Exceptions;

public sealed class InvalidImageFileTypeDomainException : DomainExceptionBase
{
    public InvalidImageFileTypeDomainException(string message)
        : base(message)
    {
    }
}
