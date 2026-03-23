using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.ValueObjects;

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Repositories;

public sealed class PlaybackSessionRedisRepository
    : IPlaybackSessionRepository
{
    private const string SessionKeyPrefix = "playback_session:";
    private const string QueueKeyPrefix = "playback_queue:";

    private readonly JsonSerializerOptions _jsonOptions;
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _options = new()
    {
        SlidingExpiration = TimeSpan.FromHours(24)
    };

    public PlaybackSessionRedisRepository(IDistributedCache cache)
    {
        _cache = cache;

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
    }

    public async Task<PlaybackSession?> GetByUserIdAsync(
        UserId userId,
        CancellationToken cancellationToken = default)
    {
        string sessionKey = GetSessionKey(userId.Value);
        string queueKey = GetQueueKey(userId.Value);

        // 1. Запускаємо обидва запити до Redis паралельно
        Task<string?> sessionTask = _cache.GetStringAsync(sessionKey, cancellationToken);
        Task<string?> queueTask = _cache.GetStringAsync(queueKey, cancellationToken);

        await Task.WhenAll(sessionTask, queueTask);

        string? sessionJson = sessionTask.Result;
        string? queueJson = queueTask.Result;
        if (string.IsNullOrEmpty(sessionJson))
        {
            return null;
        }

        // 2. Десеріалізуємо основу (SessionCore)
        PlaybackSessionSnapshot snapshot = JsonSerializer.Deserialize<PlaybackSessionSnapshot>(
            sessionJson, _jsonOptions)
            ?? throw new JsonException("Failed to deserialize JSON into a PlaybackSession object.");

        // 3. Десеріалізуємо чергу, якщо вона є
        if (!string.IsNullOrEmpty(queueJson))
        {
            IEnumerable<Guid> queueSnapshot = JsonSerializer.Deserialize<IEnumerable<Guid>>(
                queueJson, _jsonOptions)
            ?? throw new JsonException("Failed to deserialize JSON into a PlaybackQueue object.");

            snapshot = snapshot with { Queue = queueSnapshot };
        }

        return PlaybackSession.FromSnapshot(snapshot);
    }

    public async Task SaveAsync(
        PlaybackSession session,
        CancellationToken cancellationToken = default)
    {
        PlaybackSessionSnapshot sessionSnapshot = session.ToSnapshot();
        var sessionWithoutQueue = new
        {
            sessionSnapshot.Id,
            sessionSnapshot.UserId,
            sessionSnapshot.TrackId,
            sessionSnapshot.DeviceId,
            sessionSnapshot.ContextType,
            sessionSnapshot.ContextExternalId,
            sessionSnapshot.CurrentPositionMs,
            sessionSnapshot.IsPlaying,
            sessionSnapshot.Shuffle,
            sessionSnapshot.RepeatMode,
            sessionSnapshot.UpdatedAtUtc
        };

        string sessionJson = JsonSerializer.Serialize(sessionWithoutQueue, _jsonOptions);
        string sessionKey = GetSessionKey(sessionSnapshot.UserId);
        await _cache.SetStringAsync(sessionKey, sessionJson, _options, cancellationToken);

        string queueJson = JsonSerializer.Serialize(sessionSnapshot.Queue, _jsonOptions);
        string queueKey = GetQueueKey(sessionSnapshot.UserId);
        await _cache.SetStringAsync(queueKey, queueJson, _options, cancellationToken);
    }

    public async Task DeleteSessionAsync(
        UserId userId,
        CancellationToken cancellationToken = default)
    {
        string key = GetSessionKey(userId.Value);
        await _cache.RemoveAsync(key, cancellationToken);
    }

    public async Task DeleteQueueAsync(
        UserId userId,
        CancellationToken cancellationToken = default)
    {
        string key = GetQueueKey(userId.Value);
        await _cache.RemoveAsync(key, cancellationToken);
    }

    private static string GetSessionKey(Guid userId) => $"{SessionKeyPrefix}{userId}";
    private static string GetQueueKey(Guid userId) => $"{QueueKeyPrefix}{userId}";
}
