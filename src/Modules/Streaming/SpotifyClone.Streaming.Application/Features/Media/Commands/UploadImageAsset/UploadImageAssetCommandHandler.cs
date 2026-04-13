using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Abstractions.Services;
using SpotifyClone.Streaming.Application.Errors;
using SpotifyClone.Streaming.Application.Jobs;
using SpotifyClone.Streaming.Domain.Aggregates.ImageAssets;

namespace SpotifyClone.Streaming.Application.Features.Media.Commands.UploadImageAsset;

internal sealed class UploadImageAssetCommandHandler(
    IStreamingUnitOfWork unit,
    IFileStorage storage,
    IBackgroundJobService jobService)
    : ICommandHandler<UploadImageAssetCommand, UploadImageAssetCommandResult>
{
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly IFileStorage _storage = storage;
    private readonly IBackgroundJobService _jobService = jobService;

    public async Task<Result<UploadImageAssetCommandResult>> Handle(
        UploadImageAssetCommand request,
        CancellationToken cancellationToken)
    {
        var imageId = Guid.NewGuid();
        string extension = Path.GetExtension(request.FileName);
        string relativeSrcPath = $"{imageId}/source{extension}";
        
        try
        {
            await _storage.SaveTempFileToLocal(request.FileStream, relativeSrcPath);

            _jobService.Enqueue<ImageConversionJob>(job
                => job.ProcessAsync(request.FileName, imageId));
        }
        catch (DomainExceptionBase)
        {
            throw;
        }
        catch
        {
            return Result.Failure<UploadImageAssetCommandResult>(MediaErrors.ImageUploadFailed);
        }
        
        var imageAsset = ImageAsset.Create(ImageId.From(imageId), false, null);
        await _unit.ImageAssets.AddAsync(imageAsset, cancellationToken);
        
        return new UploadImageAssetCommandResult(imageId);
    }
}
