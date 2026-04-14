using Microsoft.Extensions.Logging;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Abstractions.Services;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets;

namespace SpotifyClone.Streaming.Application.Jobs;

public sealed class AudioAssetCleanupJob(
    IStreamingUnitOfWork unit,
    IFileStorage storage,
    ILogger<AudioAssetCleanupJob> logger)
{
    private readonly IFileStorage _storage = storage;
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly ILogger<AudioAssetCleanupJob> _logger = logger;

    public async Task ProcessAsync(
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Starting background job {Job}...",
            typeof(AudioAssetCleanupJob).Name);

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
            typeof(AudioAssetCleanupJob).Name);
    }
}
