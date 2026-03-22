using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.ValueObjects;

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Repositories;

public sealed class PlaybackSessionRedisRepository
    : IPlaybackSessionRepository
{
    private const string KeyPrefix = "playback_session:";

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
        string key = GetKey(userId.Value);
        string? json = await _cache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrEmpty(json))
        {
            return null;
        }

        PlaybackSessionSnapshot snapshot = JsonSerializer.Deserialize<PlaybackSessionSnapshot>(json, _jsonOptions)
            ?? throw new JsonException("Failed to deserialize JSON into a PlaybackSession object.");

        return PlaybackSession.FromSnapshot(snapshot);
    }

    public async Task SaveAsync(
        PlaybackSession session,
        CancellationToken cancellationToken = default)
    {
        PlaybackSessionSnapshot snapshot = session.ToSnapshot();
        string json = JsonSerializer.Serialize(snapshot, _jsonOptions);

        string key = GetKey(snapshot.UserId);
        await _cache.SetStringAsync(key, json, _options, cancellationToken);
    }

    public async Task DeleteAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        string key = GetKey(userId.Value);
        await _cache.RemoveAsync(key, cancellationToken);
    }

    private static string GetKey(Guid userId) => $"{KeyPrefix}{userId}";
}
