using Microsoft.EntityFrameworkCore;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions.Data;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.ValueObjects;
using SpotifyClone.Streaming.Infrastructure.Persistence.Database;

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Queries;

internal sealed class AudioAssetEfCoreReadService(
    StreamingAppDbContext context)
    : IAudioAssetReadService
{
    private readonly StreamingAppDbContext _context = context;

    public async Task<AudioAssetId?> GetByTrackId(
        TrackId trackId,
        CancellationToken cancellationToken = default)
        => await _context.AudioAssets
        .AsNoTracking()
        .Where(a => a.TrackId == trackId)
        .Select(a => a.Id)
        .SingleOrDefaultAsync(cancellationToken);
}
