using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Abstractions.Services;
using SpotifyClone.Streaming.Application.Abstractions.Services.Models;
using SpotifyClone.Streaming.Application.Errors;
using SpotifyClone.Streaming.Application.Jobs;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.ValueObjects;

namespace SpotifyClone.Streaming.Application.Features.Media.Commands.UploadAudioAsset;

internal sealed class UploadAudioAssetCommandHandler(
    IStreamingUnitOfWork unit,
    IFileStorage storage,
    IMediaService mediaService,
    IBackgroundJobService jobService)
    : ICommandHandler<UploadAudioAssetCommand, UploadAudioAssetCommandResult>
{
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly IFileStorage _storage = storage;
    private readonly IMediaService _mediaService = mediaService;
    private readonly IBackgroundJobService _jobService = jobService;

    public async Task<Result<UploadAudioAssetCommandResult>> Handle(
        UploadAudioAssetCommand request,
        CancellationToken cancellationToken)
    {
        var audioId = Guid.NewGuid();
        string extension = Path.GetExtension(request.FileName);
        string relativeSrcPath = $"{audioId}/source{extension}";
        string fullPath = Path.Combine(_storage.GetLocalConversionRootPath(), relativeSrcPath);

        try
        {
            await _storage.SaveTempFileToLocal(request.FileStream, relativeSrcPath);

            AudioMetadata metadata = await _mediaService.GetAudioMetadataAsync(fullPath);

            var audioAsset = AudioAsset.Create(
                AudioAssetId.From(audioId),
                metadata.Duration,
                null, null,
                TrackId.From(request.TrackId));

            await _unit.AudioAssets.AddAsync(audioAsset, cancellationToken);

            _jobService.Enqueue<AudioConversionJob>(job
                => job.ProcessAsync(request.FileName, audioId));

            return new UploadAudioAssetCommandResult(audioId);
        }
        catch (DomainExceptionBase)
        {
            throw;
        }
        catch
        {
            return Result.Failure<UploadAudioAssetCommandResult>(MediaErrors.AudioUploadFailed);
        }
    }
}
