using SpotifyClone.Shared.BuildingBlocks.Domain.Extensions;
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
    public TrackId? TrackId { get; private set; }
    public DeviceId DeviceId { get; private set; } = null!;
    public PlaybackContext Context { get; private set; } = null!;
    public int CurrentPositionMs { get; private set; }
    public bool IsPlaying { get; private set; }
    public bool IsShuffled { get; private set; }
    public PlaybackRepeatMode RepeatMode { get; private set; }
    public DateTimeOffset UpdatedAtUtc { get; private set; }
    public PlaybackQueue Queue { get; private set; } = null!;

    public static PlaybackSession Create(
        PlaybackSessionId id,
        UserId userId,
        TrackId? startTrackId,
        DeviceId deviceId,
        PlaybackContext context,
        DateTimeOffset nowUtc,
        IEnumerable<TrackId> tracks)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(userId);
        ArgumentNullException.ThrowIfNull(deviceId);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(tracks);

        var trackList = tracks.ToList();
        if (trackList.Count <= 0)
        {
            throw new EmptyPlaybackQueueDomainException(
                "Playback Queue must have at least one track.");
        }

        bool isShuffled = false;

        return new PlaybackSession(
            id, userId, startTrackId ?? trackList[0], deviceId, context, 0, true, isShuffled, PlaybackRepeatMode.Off,
            nowUtc.ToUniversalTime(), new PlaybackQueue(
                PlaybackQueueId.New(), trackList, trackList, startTrackId, isShuffled));
    }

    public void StartNewPlayback(
        TrackId? startTrackId,
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

        Queue.Replace(trackList, startTrackId, IsShuffled);

        TrackId = startTrackId ?? trackList[0];
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
        if (DeviceId != deviceId)
        {
            throw new InvalidDeviceDomainException("You can't seek on this device now.");
        }

        CurrentPositionMs = positionMs;
    }

    public void SkipToNext(DeviceId deviceId)
    {
        if (DeviceId != deviceId)
        {
            throw new InvalidDeviceDomainException("You can't skip to next track on this device now.");
        }

        TrackId? nextTrack = TrackId;

        if (RepeatMode != PlaybackRepeatMode.Track)
        {
            nextTrack = Queue.PopNext(RepeatMode, IsShuffled);
        }

        if (nextTrack != null)
        {
            SkipTo(nextTrack);
        }
        else
        {
            IsPlaying = false;
            TrackId = null;
            CurrentPositionMs = 0;
            UpdatedAtUtc = DateTimeOffset.UtcNow;
        }
    }

    public void ToggleShuffle(DeviceId deviceId)
    {
        if (DeviceId != deviceId)
        {
            throw new InvalidDeviceDomainException("You can't toggle Shuffle on this device now.");
        }

        if (IsShuffled)
        {
            Queue.ShuffleOff();
        }
        else
        {
            Queue.Shuffle();
        }

        IsShuffled = !IsShuffled;
    }

    public void ToggleRepeatMode(DeviceId deviceId)
    {
        if (DeviceId != deviceId)
        {
            throw new InvalidDeviceDomainException("You can't toggle Repeat mode on this device now.");
        }

        RepeatMode = RepeatMode.Next();
    }

    public void AddTrackToQueue(TrackId trackId)
        => Queue.PlayNext(trackId);

    public void RemoveTrackFromQueue(TrackId trackId)
        => Queue.Delete(trackId);

    internal void SkipTo(TrackId trackId)
    {
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
            TrackId?.Value,
            DeviceId.Value,
            Context.Type,
            Context.ExternalId,
            CurrentPositionMs,
            IsPlaying,
            IsShuffled,
            (int)RepeatMode,
            UpdatedAtUtc,
            Queue.CurrentTracks.Select(t => t.Value),
            Queue.OriginalTracks.Select(t => t.Value));

    public static PlaybackSession FromSnapshot(PlaybackSessionSnapshot snapshot)
        => new PlaybackSession(
            PlaybackSessionId.From(snapshot.Id),
            UserId.From(snapshot.UserId),
            snapshot.TrackId is null ? null : TrackId.From(snapshot.TrackId.Value),
            DeviceId.From(snapshot.DeviceId),
            PlaybackContext.From(snapshot.ContextType, snapshot.ContextExternalId),
            snapshot.CurrentPositionMs,
            snapshot.IsPlaying,
            snapshot.IsShuffled,
            (PlaybackRepeatMode)snapshot.RepeatMode,
            snapshot.UpdatedAtUtc,
            new PlaybackQueue(
                PlaybackQueueId.New(),
                snapshot.CurrentQueue.Select(t => TrackId.From(t)).ToList(),
                snapshot.OriginalQueue.Select(t => TrackId.From(t)).ToList(),
                snapshot.TrackId is null ? null : TrackId.From(snapshot.TrackId.Value),
                snapshot.IsShuffled));

    private PlaybackSession(
        PlaybackSessionId id, UserId userId, TrackId? trackId, DeviceId deviceId, PlaybackContext context,
        int currentPositionMs, bool isPlaying, bool isShuffled, PlaybackRepeatMode repeatMode,
        DateTimeOffset updatedAtUtc, PlaybackQueue queue)
        : base(id)
    {
        UserId = userId;
        TrackId = trackId;
        DeviceId = deviceId;
        Context = context;
        CurrentPositionMs = currentPositionMs;
        IsPlaying = isPlaying;
        IsShuffled = isShuffled;
        RepeatMode = repeatMode;
        UpdatedAtUtc = updatedAtUtc;
        Queue = queue;
    }

    private PlaybackSession()
    {
    }
}
