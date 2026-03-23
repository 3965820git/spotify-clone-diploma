using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.Entities;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.Enums;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.Exceptions;
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
    public PlaybackQueue Queue { get; private set; } = null!;

    public static PlaybackSession Create(
        PlaybackSessionId id,
        UserId userId,
        DeviceId deviceId,
        PlaybackContext context,
        DateTimeOffset nowUtc,
        int? positionMs,
        IEnumerable<TrackId> tracks)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(userId);
        ArgumentNullException.ThrowIfNull(deviceId);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(tracks);

        if (positionMs is not null && positionMs < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(positionMs), "Playback position must be a positive value.");
        }

        var trackList = tracks.ToList();
        if (trackList.Count <= 0)
        {
            throw new EmptyPlaybackQueueDomainException(
                "Playback Queue must have at least one track.");
        }

        return new PlaybackSession(
            id, userId, trackList[0], deviceId, context, positionMs ?? 0, true, false, PlaybackRepeatMode.Off,
            nowUtc.ToUniversalTime(), new(PlaybackQueueId.New(), trackList));
    }

    public void StartNewPlayback(
        DeviceId deviceId,
        PlaybackContext context,
        DateTimeOffset nowUtc,
        int? positionMs,
        IEnumerable<TrackId> tracks)
    {
        ArgumentNullException.ThrowIfNull(deviceId);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(tracks);

        if (positionMs is not null && positionMs < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(positionMs), "Playback position must be a positive value.");
        }

        var trackList = tracks.ToList();
        if (trackList.Count <= 0)
        {
            throw new EmptyPlaybackQueueDomainException(
                "Playback Queue must have at least one track.");
        }

        Queue.Replace(trackList);
        if (Shuffle)
        {
            Queue.Shuffle();
        }

        TrackId = trackList[0];
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

    public void SkipToNext(DeviceId deviceId)
    {
        if (DeviceId != deviceId)
        {
            return;
        }

        TrackId? nextTrack = Queue.PopNext();

        if (nextTrack != null)
        {
            SkipTo(nextTrack, deviceId);
        }
        else
        {
            IsPlaying = false;
            CurrentPositionMs = 0;
            UpdatedAtUtc = DateTimeOffset.UtcNow;
        }
    }

    internal void SkipTo(TrackId trackId, DeviceId deviceId)
    {
        if (DeviceId != deviceId)
        {
            return;
        }

        TrackId = trackId;
        CurrentPositionMs = 0;
        UpdatedAtUtc = DateTimeOffset.UtcNow;
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

    public PlaybackSessionSnapshot ToSnapshot()
        => new(
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
            UpdatedAtUtc,
            Queue.Tracks.Select(t => t.Value));

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
            snapshot.UpdatedAtUtc,
            new PlaybackQueue(PlaybackQueueId.New(), snapshot.Queue.Select(t => TrackId.From(t))));

    private PlaybackSession(
        PlaybackSessionId id, UserId userId, TrackId trackId, DeviceId deviceId, PlaybackContext context,
        int currentPositionMs, bool isPlaying, bool shuffle, PlaybackRepeatMode repeatMode,
        DateTimeOffset updatedAtUtc, PlaybackQueue queue)
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
        Queue = queue;
    }

    private PlaybackSession()
    {
    }
}
