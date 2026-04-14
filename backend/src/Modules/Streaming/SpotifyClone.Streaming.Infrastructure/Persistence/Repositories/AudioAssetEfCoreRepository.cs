using Microsoft.EntityFrameworkCore;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Enums;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.ValueObjects;
using SpotifyClone.Streaming.Infrastructure.Persistence.Database;

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Repositories;

internal sealed class AudioAssetEfCoreRepository(
    StreamingAppDbContext context)
    : IAudioAssetRepository
{
    private readonly DbSet<AudioAsset> _audioAssets = context.AudioAssets;

    public async Task AddAsync(
        AudioAsset audioAsset,
        CancellationToken cancellationToken = default)
        => await _audioAssets.AddAsync(audioAsset, cancellationToken);

    public async Task<AudioAsset?> GetByIdAsync(
        AudioAssetId id,
        CancellationToken cancellationToken = default)
        => await _audioAssets.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public async Task<IEnumerable<AudioAsset>> GetAllInvalidAsync(
        CancellationToken cancellationToken = default)
    {
        DateTimeOffset cutoffTime = DateTimeOffset.UtcNow.AddHours(-2);

        return await _audioAssets
            .Where(a => (a.Status == AudioAssetStatus.Orphaned ||
                        a.TrackId == null ||
                        a.Format == null ||
                        a.SizeInBytes == null) &&
                        a.CreatedAt < cutoffTime)
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        AudioAsset audioAsset,
        CancellationToken cancellationToken = default)
        => _audioAssets.Remove(audioAsset);
}
