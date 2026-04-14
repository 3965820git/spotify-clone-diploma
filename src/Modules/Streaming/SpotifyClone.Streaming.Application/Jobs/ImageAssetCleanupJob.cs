using Microsoft.Extensions.Logging;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Abstractions.Services;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets;

namespace SpotifyClone.Streaming.Application.Jobs;

public sealed class ImageAssetCleanupJob(
    IStreamingUnitOfWork unit,
    IFileStorage storage,
    ILogger<ImageAssetCleanupJob> logger)
{
    private readonly IFileStorage _storage = storage;
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly ILogger<ImageAssetCleanupJob> _logger = logger;

    public async Task ProcessAsync(
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Starting background job {Job}...",
            typeof(ImageAssetCleanupJob).Name);

        IEnumerable<AudioAsset> audioAssets =
            await _unit.AudioAssets.GetAllInvalidAsync(cancellationToken);

        foreach (AudioAsset audioAsset in audioAssets)
        {
            await _storage.DeleteAudioFileAsync(audioAsset.Id.Value.ToString());
            await _unit.AudioAssets.DeleteAsync(audioAsset, cancellationToken);
        }

        await _unit.CommitAsync(cancellationToken);

        _logger.LogInformation(
            "Finished background job {Job} successfully",
            typeof(ImageAssetCleanupJob).Name);
    }
}
