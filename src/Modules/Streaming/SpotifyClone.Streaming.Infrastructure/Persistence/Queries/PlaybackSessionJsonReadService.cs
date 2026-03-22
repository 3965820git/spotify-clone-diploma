using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions.Data;
using SpotifyClone.Streaming.Application.Features.Playback.Queries;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.Enums;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.ValueObjects;

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Queries;

internal sealed class PlaybackSessionJsonReadService
    : IPlaybackSessionReadService
{
    private const string KeyPrefix = "playback_session:";

    private readonly JsonSerializerOptions _jsonOptions;
    private readonly IDistributedCache _cache;

    public PlaybackSessionJsonReadService(IDistributedCache cache)
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
        string key = GetKey(userId.Value);
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

    private static string GetKey(Guid userId) => $"{KeyPrefix}{userId}";
}
