using SpotifyClone.Catalog.Domain.Aggregates.Artists.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Shared.Kernel.ValueObjects;

namespace SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;

public sealed record ArtistBannerImage : ValueObject
{
    public const int MaxWidth = 6000;
    public const int MaxHeight = 4000;
    public const long MaxSizeInBytes = 20_000_000;

    public ImageMetadata Metadata { get; init; } = null!;
    public ImageId ImageId { get; init; } = null!;

    public ArtistBannerImage(
        ImageId imageId,
        int width,
        int height,
        ImageFileType fileType,
        long sizeInBytes)
    {
        ArgumentNullException.ThrowIfNull(fileType);

        if (width * 3 == height * 7)
        {
            throw new InvalidArtistBannerImageDomainException("Banner image must have a 7:3 aspect ratio.");
        }

        if (width > MaxWidth || height > MaxHeight)
        {
            throw new InvalidArtistBannerImageDomainException(
                $"Banner image dimensions exceed maximum allowed size of {MaxWidth}x{MaxHeight} pixels.");
        }

        if (sizeInBytes > MaxSizeInBytes)
        {
            throw new InvalidArtistBannerImageDomainException(
                $"Banner image size exceeds maximum allowed size of {MaxSizeInBytes} bytes.");
        }

        Metadata = new ImageMetadata(width, height, fileType, sizeInBytes);
        ImageId = imageId;
    }

    private ArtistBannerImage()
    {
    }
}
