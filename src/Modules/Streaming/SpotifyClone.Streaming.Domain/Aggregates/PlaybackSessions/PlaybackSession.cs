using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.Enums;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.ValueObjects;
using SpotifyClone.Streaming.Domain.ValueObjects;

namespace SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions;

public sealed class PlaybackSession
    : AggregateRoot<PlaybackSessionId, Guid>
{
    public UserId UserId { get; private set; } = null!;
    public TrackId TrackId { get; private set; } = null!;
    public DeviceId DeviceId { get; private set; } = null!;
    public PlaybackContext Context { get; private set; } = null!;
    public int CurrentPositionMs { get; private set; }
    public bool IsPlaying { get; private set; }
    public bool Shuffle { get; private set; }
    public PlaybackRepeatMode RepeatMode { get; private set; }
    public DateTimeOffset UpdatedAtUtc { get; private set; }

    public static PlaybackSession Create(
        PlaybackSessionId id,
        UserId userId,
        TrackId trackId,
        DeviceId deviceId,
        PlaybackContext context,
        DateTimeOffset nowUtc,
        int? positionMs)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(userId);
        ArgumentNullException.ThrowIfNull(trackId);
        ArgumentNullException.ThrowIfNull(deviceId);
        ArgumentNullException.ThrowIfNull(context);

        if (positionMs is not null && positionMs < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(positionMs), "Playback position must be a positive value.");
        }

        return new PlaybackSession(
            id, userId, trackId, deviceId, context, positionMs ?? 0, true, false, PlaybackRepeatMode.Off,
            nowUtc.ToUniversalTime());
    }

    public void StartNewPlayback(
        TrackId trackId,
        DeviceId deviceId,
        PlaybackContext context,
        DateTimeOffset nowUtc,
        int? positionMs)
    {
        ArgumentNullException.ThrowIfNull(trackId);
        ArgumentNullException.ThrowIfNull(deviceId);
        ArgumentNullException.ThrowIfNull(context);

        if (positionMs is not null && positionMs < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(positionMs), "Playback position must be a positive value.");
        }

        TrackId = trackId;
        DeviceId = deviceId;
        Context = context;
        CurrentPositionMs = positionMs ?? 0;
        IsPlaying = true;
        UpdatedAtUtc = nowUtc.ToUniversalTime();
    }

    public void Resume(DeviceId deviceId)
    {
        TryTransferTo(deviceId);

        if (IsPlaying)
        {
            return;
        }

        IsPlaying = true;
        UpdatedAtUtc = DateTimeOffset.UtcNow.ToUniversalTime();
    }

    public void Pause(DeviceId deviceId)
    {
        TryTransferTo(deviceId);

        if (!IsPlaying)
        {
            return;
        }

        IsPlaying = false;
        UpdatedAtUtc = DateTimeOffset.UtcNow.ToUniversalTime();
    }

    public void SyncPosition(int positionMs, DeviceId deviceId)
    {
        if (DeviceId != deviceId)
        {
            return;
        }

        CurrentPositionMs = positionMs;
    }

    public void SeekTo(int positionMs, DeviceId deviceId)
    {
        TryTransferTo(deviceId);
        CurrentPositionMs = positionMs;
    }

    private void TryTransferTo(DeviceId deviceId)
    {
        if (DeviceId == deviceId)
        {
            return;
        }

        DeviceId = deviceId;
        UpdatedAtUtc = DateTimeOffset.UtcNow.ToUniversalTime();
    }

    public PlaybackSessionSnapshot ToSnapshot() => new(
        Id.Value,
        UserId.Value,
        TrackId.Value,
        DeviceId.Value,
        Context.Type,
        Context.ExternalId,
        CurrentPositionMs,
        IsPlaying,
        Shuffle,
        (int)RepeatMode,
        UpdatedAtUtc
    );

    public static PlaybackSession FromSnapshot(PlaybackSessionSnapshot snapshot)
        => new PlaybackSession(
            PlaybackSessionId.From(snapshot.Id),
            UserId.From(snapshot.UserId),
            TrackId.From(snapshot.TrackId),
            DeviceId.From(snapshot.DeviceId),
            PlaybackContext.From(snapshot.ContextType, snapshot.ContextExternalId),
            snapshot.CurrentPositionMs,
            snapshot.IsPlaying,
            snapshot.Shuffle,
            (PlaybackRepeatMode)snapshot.RepeatMode,
            snapshot.UpdatedAtUtc);

    private PlaybackSession(
        PlaybackSessionId id, UserId userId, TrackId trackId, DeviceId deviceId, PlaybackContext context,
        int currentPositionMs, bool isPlaying, bool shuffle, PlaybackRepeatMode repeatMode,
        DateTimeOffset updatedAtUtc)
        : base(id)
    {
        UserId = userId;
        TrackId = trackId;
        DeviceId = deviceId;
        Context = context;
        CurrentPositionMs = currentPositionMs;
        IsPlaying = isPlaying;
        Shuffle = shuffle;
        RepeatMode = repeatMode;
        UpdatedAtUtc = updatedAtUtc;
    }

    private PlaybackSession()
    {
    }
}
