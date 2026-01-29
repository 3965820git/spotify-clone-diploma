using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.Exceptions;

namespace SpotifyClone.Shared.Kernel.ValueObjects;

public sealed record ImageMetadata : ValueObject
{
    public int Width { get; }
    public int Height { get; }
    public ImageFileType FileType { get; }

    private ImageMetadata()
    {
        Width = default;
        Height = default;
        FileType = null!;
    }

    public ImageMetadata(int width, int height, int maxWidth, int maxHeight, ImageFileType fileType)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxWidth);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxHeight);
        ArgumentNullException.ThrowIfNull(fileType);

        if (width > maxWidth || height > maxHeight)
        {
            throw new InvalidImageMetadataDomainException($"Image must not exceed {maxWidth}x{maxHeight}.");
        }

        Width = width;
        Height = height;
        FileType = fileType;
    }
}
