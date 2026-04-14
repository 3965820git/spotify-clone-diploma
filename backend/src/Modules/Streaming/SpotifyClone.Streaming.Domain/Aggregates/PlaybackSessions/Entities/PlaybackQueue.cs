using System.Security.Cryptography;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.Enums;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.ValueObjects;

namespace SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.Entities;

public sealed class PlaybackQueue : Entity<PlaybackQueueId, Guid>
{
    private List<TrackId> _currentTracks = [];
    private List<TrackId> _previousTracks = [];
    private readonly List<TrackId> _originalTracks = [];

    public IReadOnlyList<TrackId> CurrentTracks => _currentTracks.AsReadOnly();
    public IReadOnlyList<TrackId> PreviousTracks => _previousTracks.AsReadOnly();
    public IReadOnlyList<TrackId> OriginalTracks => _originalTracks.AsReadOnly();
    public bool IsCurrentTracksEmpty => _currentTracks.Count <= 0;
    public bool IsPreviousTracksEmpty => _previousTracks.Count <= 0;

    internal PlaybackQueue(
        PlaybackQueueId id,
        List<TrackId> currentTracks,
        List<TrackId> previousTracks,
        List<TrackId> originalTracks,
        TrackId? startTrackId,
        bool isShuffled)
    {
        ArgumentNullException.ThrowIfNull(id);
        
        if (currentTracks.Count == originalTracks.Count)
        {
            Replace(originalTracks.ToList(), startTrackId, isShuffled);
        }
        else
        {
            _originalTracks = originalTracks;
            _currentTracks = currentTracks;
        }

        _previousTracks = previousTracks;
    }

    internal void Replace(List<TrackId> tracks, TrackId? startTrackId, bool isShuffled)
    {
        Clear();
        _originalTracks.AddRange(tracks);

        if (startTrackId is null && !isShuffled)
        {
            _currentTracks.AddRange(tracks.Skip(1));
            return; 
        }
        
        if (startTrackId is not null)
        {
            if (isShuffled)
            {
                tracks.Remove(startTrackId);
            }
            else
            {
                int startTrackIndex = _originalTracks.IndexOf(startTrackId);
                if (startTrackIndex > -1)
                {
                    tracks.RemoveRange(0, startTrackIndex + 1);
                }
            }

            _currentTracks.AddRange(tracks);
        }

        if (isShuffled)
        {
            Shuffle();
        }
    }

    internal void PushToNext(TrackId trackId)
    {
        _currentTracks.Remove(trackId);
        _currentTracks.Insert(0, trackId);
    }

    internal TrackId? PopNext(PlaybackRepeatMode repeatMode, bool isShuffled)
    {
        if (IsCurrentTracksEmpty)
        {
            if (repeatMode == PlaybackRepeatMode.All)
            {
                _currentTracks = [.. _originalTracks];

                if (isShuffled)
                {
                    Shuffle();
                }
            }
            else if (repeatMode == PlaybackRepeatMode.Off)
            {
                return null;
            }
        }

        TrackId nextTrack = _currentTracks[0];
        _currentTracks.RemoveAt(0);

        return nextTrack;
    }

    internal void PushToPrevious(TrackId finishedTrackId)
    {
        _previousTracks.Remove(finishedTrackId);
        _previousTracks.Add(finishedTrackId);
    }

    internal TrackId? PopPrevious(PlaybackRepeatMode repeatMode, bool isShuffled)
    {
        if (IsPreviousTracksEmpty)
        {
            if (repeatMode == PlaybackRepeatMode.All)
            {
                _previousTracks = [.. _originalTracks];

                if (isShuffled)
                {
                    Shuffle();
                }
            }
            else if (repeatMode == PlaybackRepeatMode.Off)
            {
                return null;
            }
        }

        TrackId previousTrack = _previousTracks[^1];
        _previousTracks.Remove(previousTrack);

        return previousTrack;
    }

    internal bool Delete(TrackId trackId)
    {
        bool deletedFromCurrentTracks = _currentTracks.Remove(trackId);
        bool deletedFromPreviousTracks = _currentTracks.Remove(trackId);
        bool deletedFromOriginalTracks = _originalTracks.Remove(trackId);

        return deletedFromCurrentTracks && deletedFromPreviousTracks && deletedFromOriginalTracks;
    }

    internal void Clear()
    {
        _currentTracks.Clear();
        _previousTracks.Clear();
        _originalTracks.Clear();
    }

    internal void Shuffle()
    {
        if (_currentTracks.Count <= 1)
        {
            return;
        }

        int n = _currentTracks.Count;
        while (n > 1)
        {
            n--;
            int k = RandomNumberGenerator.GetInt32(n + 1);
            (_currentTracks[k], _currentTracks[n]) = (_currentTracks[n], _currentTracks[k]);
        }
    }

    internal void ShuffleOff()
    {
        var remainingTracks = _originalTracks
            .Where(t => _currentTracks.Contains(t))
            .ToList();

        _currentTracks.Clear();
        _currentTracks.AddRange(remainingTracks);
    }

    private PlaybackQueue()
    {
    }
}
