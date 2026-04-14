using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Streaming.Domain.Aggregates.ImageAssets;

public interface IImageAssetRepository
{
    Task<ImageAsset?> GetByIdAsync(
        ImageId id,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ImageAsset>> GetAllInvalidAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        ImageAsset imageAsset,
        CancellationToken cancellationToken = default);
}
