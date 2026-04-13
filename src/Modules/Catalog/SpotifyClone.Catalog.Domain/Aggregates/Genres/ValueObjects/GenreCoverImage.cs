using SpotifyClone.Catalog.Domain.Aggregates.Genres.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Shared.Kernel.ValueObjects;

namespace SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;

public sealed record GenreCoverImage : ValueObject
{
    public const int MaxWidth = 3000;
    public const int MaxHeight = 3000;
    public const long MaxSizeInBytes = 4_000_000;

    public ImageMetadata Metadata { get; init; } = null!;
    public ImageId ImageId { get; init; } = null!;

    public GenreCoverImage(
        ImageId imageId,
        int width,
        int height,
        ImageFileType fileType,
        long sizeInBytes)
    {
        ArgumentNullException.ThrowIfNull(fileType);

        if (width != height)
        {
            throw new InvalidGenreCoverImageDomainException("Genre cover image must be square.");
        }

        if (width > MaxWidth || height > MaxHeight)
        {
            throw new InvalidGenreCoverImageDomainException(
                $"Genre cover image dimensions exceed maximum allowed size of {MaxWidth}x{MaxHeight} pixels.");
        }

        if (sizeInBytes > MaxSizeInBytes)
        {
            throw new InvalidGenreCoverImageDomainException(
                $"Genre cover image size exceeds maximum allowed size of {MaxSizeInBytes} bytes.");
        }

        Metadata = new ImageMetadata(width, height, fileType, sizeInBytes);
        ImageId = imageId;
    }

    private GenreCoverImage()
    {
    }
}
