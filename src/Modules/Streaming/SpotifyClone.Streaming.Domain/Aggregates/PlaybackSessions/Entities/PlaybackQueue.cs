using System.Security.Cryptography;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.ValueObjects;

namespace SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.Entities;

public sealed class PlaybackQueue : Entity<PlaybackQueueId, Guid>
{
    private readonly List<TrackId> _tracks = new();

    public IReadOnlyList<TrackId> Tracks => _tracks.AsReadOnly();
    public bool IsEmpty => _tracks.Count == 0;

    internal PlaybackQueue(PlaybackQueueId id, IEnumerable<TrackId> tracks)
    {
        ArgumentNullException.ThrowIfNull(id);

        _tracks.AddRange(tracks);
    }

    public void Enqueue(TrackId trackId)
        => _tracks.Add(trackId);

    public void PlayNext(TrackId trackId)
        => _tracks.Insert(0, trackId);

    public TrackId? PopNext()
    {
        if (IsEmpty)
        {
            return null;
        }

        TrackId nextTrack = _tracks[0];
        _tracks.RemoveAt(0);
        return nextTrack;
    }

    public void Clear()
        => _tracks.Clear();

    public void Shuffle()
    {
        // Простий алгоритм Фішера-Йейтса для перемішування
        int n = _tracks.Count;
        while (n > 1)
        {
            n--;
            int k = RandomNumberGenerator.GetInt32(n + 1);
            (_tracks[k], _tracks[n]) = (_tracks[n], _tracks[k]);
        }
    }

    private PlaybackQueue()
    {
    }
}
