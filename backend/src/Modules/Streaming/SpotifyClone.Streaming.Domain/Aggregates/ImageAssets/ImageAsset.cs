using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Shared.Kernel.ValueObjects;

namespace SpotifyClone.Streaming.Domain.Aggregates.ImageAssets;

public sealed class ImageAsset : AggregateRoot<ImageId, Guid>
{
    public ImageMetadata? Metadata { get; private set; }
    public bool IsReady { get; private set; }
    public int LinkCount { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public static ImageAsset Create(ImageId id, bool isReady, ImageMetadata? metadata)
    {
        ArgumentNullException.ThrowIfNull(id);

        return new ImageAsset(id, metadata, isReady, 0, DateTimeOffset.UtcNow);
    }

    public void MarkAsReady(ImageMetadata metadata)
    {
        ArgumentNullException.ThrowIfNull(metadata);

        if (IsReady)
        {
            return;
        }

        Metadata = metadata;
        IsReady = true;
    }

    public void AddLink()
        => LinkCount++;

    public void RemoveLink()
    {
        if (LinkCount > 0)
        {
            LinkCount--;
        }
    }

    private ImageAsset(
        ImageId id,
        ImageMetadata? metadata,
        bool isReady,
        int linkCount,
        DateTimeOffset createdAt)
        : base(id)
    {
        Metadata = metadata;
        IsReady = isReady;
        LinkCount = linkCount;
        CreatedAt = createdAt;
    }

    private ImageAsset()
    {
    }
}
