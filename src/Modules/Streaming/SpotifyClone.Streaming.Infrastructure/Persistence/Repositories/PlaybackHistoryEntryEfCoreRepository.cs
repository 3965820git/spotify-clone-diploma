using Microsoft.EntityFrameworkCore;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackHistoryEntries;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackHistoryEntries.ValueObjects;
using SpotifyClone.Streaming.Infrastructure.Persistence.Database;

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Repositories;

internal sealed class PlaybackHistoryEntryEfCoreRepository(
    StreamingAppDbContext context)
    : IPlaybackHistoryEntryRepository
{
    private readonly DbSet<PlaybackHistoryEntry> _playbackHistoryEntries = context.PlaybackHistoryEntries;

    public async Task AddAsync(
        PlaybackHistoryEntry playbackHistoryEntry,
        CancellationToken cancellationToken = default)
        => await _playbackHistoryEntries.AddAsync(playbackHistoryEntry, cancellationToken);

    public async Task GetById(
        PlaybackHistoryEntryId id,
        CancellationToken cancellationToken = default)
        => await _playbackHistoryEntries
            .Where(e => e.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
}
