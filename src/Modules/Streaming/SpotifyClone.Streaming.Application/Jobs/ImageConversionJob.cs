using Microsoft.Extensions.Logging;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Abstractions.Services;
using SpotifyClone.Streaming.Domain.Aggregates.ImageAssets;

namespace SpotifyClone.Streaming.Application.Jobs;

public sealed class ImageConversionJob(
    IStreamingUnitOfWork unit,
    IMediaService mediaService,
    IFileStorage storage,
    ILogger<ImageConversionJob> logger)
{
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly IMediaService _mediaService = mediaService;
    private readonly IFileStorage _storage = storage;
    private readonly ILogger<ImageConversionJob> _logger = logger;

    public async Task ProcessAsync(
        string fileName,
        Guid imageId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting background conversion for {ImageId}", imageId);

        string extension = Path.GetExtension(fileName);
        string workDir = imageId.ToString();
        string localTempDir = Path.Combine(_storage.GetLocalConversionRootPath(), workDir);
        string srcPath = Path.Combine(
            _storage.GetLocalConversionRootPath(),
            $"{imageId}/source{extension}");
        string relativePath;
        string localConvertedFilePath = string.Empty;
        Abstractions.Services.Models.ImageMetadata? metadata = null;

        if (!Directory.Exists(localTempDir))
        {
            Directory.CreateDirectory(localTempDir);
        }

        try
        {
            Result convertResult = await _mediaService.ConvertToWebpAsync(srcPath, imageId);
            if (convertResult.IsFailure)
            {
                _logger.LogError("Conversion failed: {Error}", convertResult.Errors[0].Description);
                throw new Exception($"Conversion failed: {convertResult.Errors[0].Description}");
            }

            string[] files = Directory.GetFiles(localTempDir, "*", SearchOption.AllDirectories);
            foreach (string fullPath in files)
            {
                if (string.Equals(fullPath, srcPath, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                localConvertedFilePath = fullPath;
                string relative = Path.GetRelativePath(localTempDir, fullPath);
                relativePath = $"{imageId}/{relative.Replace(Path.DirectorySeparatorChar, '/')}";
                await using FileStream fs = File.OpenRead(fullPath);
                await _storage.SaveImageFileAsync(fs, relativePath);
            }

            metadata = await _mediaService.GetImageMetadataAsync(localConvertedFilePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Critical error in background job while converting");
            throw;
        }
        finally
        {
            await _storage.DeleteTempDirectoryFromLocal(workDir);
        }

        if (metadata is null)
        {
            _logger.LogError("Failed to get metadata from {ImageId}", imageId);
            throw new Exception($"Failed to get metadata from {imageId}");
        }

        ImageAsset? imageAsset = await _unit.ImageAssets.GetByIdAsync(ImageId.From(imageId), cancellationToken);
        if (imageAsset is null)
        {
            _logger.LogError("Image asset not found for {ImageId}", imageId);
            throw new Exception($"Image asset not found for {imageId}");
        }

        imageAsset.MarkAsReady(new(
            metadata.Width,
            metadata.Height,
            ImageFileType.From(metadata.FileType),
            metadata.SizeInBytes));

        await _unit.CommitAsync(cancellationToken);

        _logger.LogInformation("Job finished successfully for {ImageId}", imageId);
    }
}
