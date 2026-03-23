using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions.Data;
using SpotifyClone.Streaming.Application.Features.Playback.Queries;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.Enums;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.ValueObjects;

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Queries;

internal sealed class PlaybackSessionRedisReadService
    : IPlaybackSessionReadService
{
    private const string SessionKeyPrefix = "playback_session:";
    private const string QueueKeyPrefix = "playback_queue:";

    private readonly JsonSerializerOptions _jsonOptions;
    private readonly IDistributedCache _cache;

    public PlaybackSessionRedisReadService(IDistributedCache cache)
    {
        _cache = cache;

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
    }

    public async Task<PlaybackSessionDetails?> GetDetails(
        UserId userId,
        CancellationToken cancellationToken = default)
    {
        string key = GetSessionKey(userId.Value);
        string? json = await _cache.GetStringAsync(key, cancellationToken);
        if (string.IsNullOrEmpty(json))
        {
            return null;
        }

        PlaybackSessionSnapshot? snapshot = JsonSerializer.Deserialize<PlaybackSessionSnapshot>(json, _jsonOptions);
        if (snapshot is null)
        {
            return null;
        }

        return new PlaybackSessionDetails(
            snapshot.Id,
            snapshot.UserId,
            snapshot.TrackId,
            snapshot.DeviceId,
            snapshot.ContextType,
            snapshot.ContextExternalId,
            snapshot.CurrentPositionMs,
            snapshot.IsPlaying,
            snapshot.Shuffle,
            ((PlaybackRepeatMode)snapshot.RepeatMode).ToString(),
            snapshot.UpdatedAtUtc);
    }

    public async Task<IEnumerable<Guid>> GetQueue(
        UserId userId,
        CancellationToken cancellationToken = default)
    {
        string key = GetQueueKey(userId.Value);
        string? json = await _cache.GetStringAsync(key, cancellationToken);
        if (string.IsNullOrEmpty(json))
        {
            return [];
        }

        return JsonSerializer.Deserialize<IEnumerable<Guid>>(json, _jsonOptions) ?? [];
    }

    private static string GetSessionKey(Guid userId) => $"{SessionKeyPrefix}{userId}";
    private static string GetQueueKey(Guid userId) => $"{QueueKeyPrefix}{userId}";
}
