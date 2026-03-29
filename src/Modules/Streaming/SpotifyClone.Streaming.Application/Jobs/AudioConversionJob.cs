using Microsoft.Extensions.Logging;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Abstractions.Services;
using SpotifyClone.Streaming.Application.Abstractions.Services.Models;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Enums;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.ValueObjects;

namespace SpotifyClone.Streaming.Application.Jobs;

public sealed class AudioConversionJob(
    IStreamingUnitOfWork unit,
    IMediaService mediaService,
    IFileStorage storage,
    ILogger<AudioConversionJob> logger)
{
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly IMediaService _mediaService = mediaService;
    private readonly IFileStorage _storage = storage;
    private readonly ILogger<AudioConversionJob> _logger = logger;

    public async Task ProcessAsync(
        string fileName,
        Guid audioId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting background conversion for {AudioId}", audioId);

        string extension = Path.GetExtension(fileName);
        string workDir = audioId.ToString();
        string localTempDir = Path.Combine(_storage.GetLocalConversionRootPath(), workDir);
        string srcPath = Path.Combine(
            _storage.GetLocalConversionRootPath(),
            $"{audioId}/source{extension}");
        long totalSizeInBytes = 0;
        AudioMetadata? metadata = null;

        if (!Directory.Exists(localTempDir))
        {
            Directory.CreateDirectory(localTempDir);
        }

        try
        {
            Result result = await _mediaService.ConvertToHlsDashAsync(srcPath, audioId);
            if (result.IsFailure)
            {
                _logger.LogError("Conversion failed: {Error}", result.Errors[0].Description);
                throw new Exception($"Conversion failed: {result.Errors[0].Description}");
            }

            string[] files = Directory.GetFiles(localTempDir, "*", SearchOption.AllDirectories);
            foreach (string fullPath in files)
            {
                if (string.Equals(fullPath, srcPath, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var fileInfo = new FileInfo(fullPath);
                totalSizeInBytes += fileInfo.Length;

                string relative = Path.GetRelativePath(localTempDir, fullPath);
                string relativePath = $"{audioId}/{relative.Replace(Path.DirectorySeparatorChar, '/')}";
                await using FileStream fs = File.OpenRead(fullPath);
                await _storage.SaveAudioFileAsync(fs, relativePath);
            }

            metadata = await _mediaService.GetAudioMetadataAsync(srcPath);
        }
        catch (Exception ex)
        {
            await _storage.DeleteAudioFileAsync(workDir);
            _logger.LogError(ex, "Critical error in background job while converting");
            throw;
        }
        finally
        {
            await _storage.DeleteTempDirectoryFromLocal(workDir);
        }

        if (metadata is null)
        {
            _logger.LogError("Failed to get metadata from {AudioId}", audioId);
            throw new Exception($"Failed to get metadata from {audioId}");
        }

        AudioMetadata finalMetadata = metadata with { SizeInBytes = totalSizeInBytes };
        if (finalMetadata is null)
        {
            _logger.LogError("Failed to get metadata from {AudioId}", audioId);
            throw new Exception($"Failed to get metadata from {audioId}");
        }

        AudioAsset? audioAsset = null;
        for (int i = 0; audioAsset is null && i < 10; i++)
        {
            await Task.Delay(1000, cancellationToken);
            audioAsset = await _unit.AudioAssets.GetByIdAsync(AudioAssetId.From(audioId), cancellationToken);
        }

        if (audioAsset is null)
        {
            _logger.LogError("Audio asset not found for {AudioId}", audioId);
            throw new Exception($"Audio asset not found for {audioId}");
        }

        audioAsset.MarkAsUploaded(
            metadata.Duration,
            AudioFormat.From(metadata.Format),
            metadata.SizeInBytes);

        await _unit.CommitAsync(cancellationToken);

        _logger.LogInformation("Job finished successfully for {AudioId}", audioId);
    }
}
