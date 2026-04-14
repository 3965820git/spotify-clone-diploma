using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.Kernel.Exceptions;

public sealed class InvalidImageMetadataDomainException : DomainExceptionBase
{
    public InvalidImageMetadataDomainException(string message)
        : base(message)
    {
    }
}
