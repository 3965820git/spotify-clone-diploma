using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.ValueObjects;

namespace SpotifyClone.Streaming.Domain.Aggregates.AudioAssets;

public interface IAudioAssetRepository
{
    Task<AudioAsset?> GetByIdAsync(
        AudioAssetId id,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        AudioAsset audioAsset,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<AudioAsset>> GetAllInvalidAsync(
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        AudioAsset audioAsset,
        CancellationToken cancellationToken = default);
}
