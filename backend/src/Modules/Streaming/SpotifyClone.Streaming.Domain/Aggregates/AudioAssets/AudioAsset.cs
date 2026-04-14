using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Enums;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Events;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Exceptions;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.ValueObjects;

namespace SpotifyClone.Streaming.Domain.Aggregates.AudioAssets;

public sealed class AudioAsset : AggregateRoot<AudioAssetId, Guid>
{
    public static readonly TimeSpan MaxDuration = TimeSpan.FromHours(24);

    public TimeSpan Duration { get; private set; }
    public AudioFormat? Format { get; private set; }
    public long? SizeInBytes { get; private set; }
    public AudioAssetStatus Status { get; private set; } = null!;
    public DateTimeOffset CreatedAt { get; private set; }
    public TrackId? TrackId { get; private set; }

    public static AudioAsset Create(
        AudioAssetId id,
        TimeSpan duration,
        AudioFormat? format,
        long? sizeInBytes,
        TrackId trackId)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(trackId);

        if (duration > MaxDuration)
        {
            throw new InvalidDurationDomainException($"Duration cannot exceed {MaxDuration.TotalHours} hours.");
        }

        var audioAsset = new AudioAsset(
            id, duration, format, sizeInBytes, AudioAssetStatus.Uploading, DateTimeOffset.UtcNow, trackId);

        audioAsset.RaiseDomainEvent(new AudioAssetCreatedDomainEvent(id, trackId, duration));

        return audioAsset;
    }

    public void MarkAsUploaded(TimeSpan duration, AudioFormat format, long sizeInBytes)
    {
        if (Status.IsUploaded)
        {
            return;
        }

        if (TrackId is null)
        {
            throw new TrackNotLinkedDomainException(
                "Audio Asset cannot be ready without a linked Track.");
        }

        if (duration <= TimeSpan.Zero)
        {
            throw new InvalidDurationDomainException("Duration must be greater than zero.");
        }

        ArgumentNullException.ThrowIfNull(format);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sizeInBytes);

        Duration = duration;
        Format = format;
        SizeInBytes = sizeInBytes;
        Status = AudioAssetStatus.Uploaded;

        RaiseDomainEvent(new AudioAssetUploadedDomainEvent(TrackId));
    }

    public void MarkAsOrphaned()
    {
        TrackId = null;
        Status = AudioAssetStatus.Orphaned;
    }

    private AudioAsset(
        AudioAssetId id,
        TimeSpan duration,
        AudioFormat? format,
        long? sizeInBytes,
        AudioAssetStatus status,
        DateTimeOffset createdAt,
        TrackId? trackId)
        : base(id)
    {
        Duration = duration;
        Format = format;
        SizeInBytes = sizeInBytes;
        Status = status;
        CreatedAt = createdAt;
        TrackId = trackId;
    }

    private AudioAsset()
    {
    }
}
