using SpotifyClone.Catalog.Domain.Aggregates.Artists.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Shared.Kernel.ValueObjects;

namespace SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;

public sealed record ArtistAvatarImage : ValueObject
{
    public const int MaxWidth = 750;
    public const int MaxHeight = 750;
    public const long MaxSizeInBytes = 20_000_000;

    public ImageMetadata Metadata { get; init; } = null!;
    public ImageId ImageId { get; init; } = null!;

    public ArtistAvatarImage(
        ImageId imageId,
        int width,
        int height,
        ImageFileType fileType,
        long sizeInBytes)
    {
        ArgumentNullException.ThrowIfNull(fileType);

        if (width != height)
        {
            throw new InvalidArtistAvatarImageDomainException("Avatar image must be square.");
        }

        if (width > MaxWidth || height > MaxHeight)
        {
            throw new InvalidArtistAvatarImageDomainException(
                $"Avatar image dimensions exceed maximum allowed size of {MaxWidth}x{MaxHeight} pixels.");
        }

        if (sizeInBytes > MaxSizeInBytes)
        {
            throw new InvalidArtistAvatarImageDomainException(
                $"Avatar image size exceeds maximum allowed size of {MaxSizeInBytes} bytes.");
        }

        Metadata = new ImageMetadata(width, height, fileType, sizeInBytes);
        ImageId = imageId;
    }

    private ArtistAvatarImage()
    {
    }
}
