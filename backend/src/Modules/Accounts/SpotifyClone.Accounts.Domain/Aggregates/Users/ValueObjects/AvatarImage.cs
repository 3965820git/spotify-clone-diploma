using SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Shared.Kernel.ValueObjects;

namespace SpotifyClone.Accounts.Domain.Aggregates.Users.ValueObjects;

public sealed record AvatarImage : ValueObject
{
    public const int MaxWidth = 750;
    public const int MaxHeight = 750;
    public const long MaxSizeInBytes = 4_000_000;

    public ImageMetadata Metadata { get; init; } = null!;
    public ImageId ImageId { get; init; } = null!;

    public AvatarImage(
        ImageId imageId,
        int width,
        int height,
        ImageFileType fileType,
        long sizeInBytes)
    {
        ArgumentNullException.ThrowIfNull(fileType);

        if (width != height)
        {
            throw new InvalidAvatarImageDomainException("Avatar image must be square.");
        }

        if (width > MaxWidth || height > MaxHeight)
        {
            throw new InvalidAvatarImageDomainException(
                $"Avatar image dimensions exceed maximum allowed size of {MaxWidth}x{MaxHeight} pixels.");
        }

        if (sizeInBytes > MaxSizeInBytes)
        {
            throw new InvalidAvatarImageDomainException(
                $"Avatar image size exceeds maximum allowed size of {MaxSizeInBytes} bytes.");
        }

        Metadata = new ImageMetadata(width, height, fileType, sizeInBytes);
        ImageId = imageId;
    }

    private AvatarImage()
    {
    }
}
