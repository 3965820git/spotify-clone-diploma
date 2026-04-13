using Microsoft.EntityFrameworkCore;
using SpotifyClone.Streaming.Application.Abstractions.Data;
using SpotifyClone.Streaming.Application.Features.Playback.Queries;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackHistoryEntries.ValueObjects;
using SpotifyClone.Streaming.Infrastructure.Persistence.Database;

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Queries;

internal sealed class PlaybackHistoryEntryEfCoreReadService(
    StreamingAppDbContext context)
    : IPlaybackHistoryEntryReadService
{
    public readonly StreamingAppDbContext _context = context;

    public async Task<PlaybackHistoryEntryDetails?> GetDetailsAsync(
        PlaybackHistoryEntryId id,
        CancellationToken cancellationToken = default)
        => await _context.PlaybackHistoryEntries
        .AsNoTracking()
        .Where(e => e.Id == id)
        .Select(e => new PlaybackHistoryEntryDetails(
            e.Id.Value,
            e.UserId.Value,
            e.TrackId.Value,
            e.Context.Type,
            e.Context.ExternalId == null ? null : e.Context.ExternalId.Value,
            e.PlayedAtUtc))
        .SingleOrDefaultAsync(cancellationToken);
}
