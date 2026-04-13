using SpotifyClone.Playlists.Domain.Aggregates.Playlists.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Shared.Kernel.ValueObjects;

namespace SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;

public sealed record PlaylistCoverImage : ValueObject
{
    public const int MaxWidth = 3000;
    public const int MaxHeight = 3000;
    public const long MaxSizeInBytes = 4_000_000;

    public ImageMetadata Metadata { get; init; } = null!;
    public ImageId ImageId { get; init; } = null!;

    private PlaylistCoverImage()
    {
    }

    public PlaylistCoverImage(
        ImageId imageId,
        int width,
        int height,
        ImageFileType fileType,
        long sizeInBytes)
    {
        ArgumentNullException.ThrowIfNull(fileType);

        if (width != height)
        {
            throw new InvalidPlaylistCoverImageDomainException("Cover image must be square.");
        }

        if (width > MaxWidth || height > MaxHeight)
        {
            throw new InvalidPlaylistCoverImageDomainException(
                $"Cover image dimensions exceed maximum allowed size of {MaxWidth}x{MaxHeight} pixels.");
        }

        if (sizeInBytes > MaxSizeInBytes)
        {
            throw new InvalidPlaylistCoverImageDomainException(
                $"Cover image size exceeds maximum allowed size of {MaxSizeInBytes} bytes.");
        }

        Metadata = new ImageMetadata(width, height, fileType, sizeInBytes);
        ImageId = imageId;
    }
}
