using SpotifyClone.Catalog.Domain.Aggregates.Moods.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Shared.Kernel.ValueObjects;

namespace SpotifyClone.Catalog.Domain.Aggregates.Moods.ValueObjects;

public sealed record MoodCoverImage : ValueObject
{
    public const int MaxWidth = 3000;
    public const int MaxHeight = 3000;
    public const long MaxSizeInBytes = 4_000_000;

    public ImageMetadata Metadata { get; init; } = null!;
    public ImageId ImageId { get; init; } = null!;

    public MoodCoverImage(
        ImageId imageId,
        int width,
        int height,
        ImageFileType fileType,
        long sizeInBytes)
    {
        ArgumentNullException.ThrowIfNull(fileType);

        if (width != height)
        {
            throw new InvalidMoodCoverImageDomainException("Mood cover image must be square.");
        }

        if (width > MaxWidth || height > MaxHeight)
        {
            throw new InvalidMoodCoverImageDomainException(
                $"Mood cover image dimensions exceed maximum allowed size of {MaxWidth}x{MaxHeight} pixels.");
        }

        if (sizeInBytes > MaxSizeInBytes)
        {
            throw new InvalidMoodCoverImageDomainException(
                $"Mood cover image size exceeds maximum allowed size of {MaxSizeInBytes} bytes.");
        }

        Metadata = new ImageMetadata(width, height, fileType, sizeInBytes);
        ImageId = imageId;
    }

    private MoodCoverImage()
    {
    }
}
