using Microsoft.Extensions.Logging;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Errors;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.ValueObjects;

namespace SpotifyClone.Streaming.Application.Features.Media.Commands.MarkAudioAssetAsOrphaned;

internal sealed class MarkAudioAssetAsOrphanedCommandHandler(
    IStreamingUnitOfWork unit,
    ILogger<MarkAudioAssetAsOrphanedCommandHandler> logger)
    : ICommandHandler<MarkAudioAssetAsOrphanedCommand, MarkAudioAssetAsOrphanedCommandResult>
{
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly ILogger<MarkAudioAssetAsOrphanedCommandHandler> _logger = logger;

    public async Task<Result<MarkAudioAssetAsOrphanedCommandResult>> Handle(
        MarkAudioAssetAsOrphanedCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Marking Audio Asset {AudioAssetId} as orphaned", request.AudioAssetId);

        AudioAsset? audioAsset = await _unit.AudioAssets.GetByIdAsync(
            AudioAssetId.From(request.AudioAssetId),
            cancellationToken);

        if (audioAsset is null)
        {
            _logger.LogWarning(
                "Audio Asset {AudioAssetId} not found while marking as orphaned",
                request.AudioAssetId);

            return Result.Failure<MarkAudioAssetAsOrphanedCommandResult>(MediaErrors.MediaAssetNotFound);
        }

        audioAsset.MarkAsOrphaned();

        return new MarkAudioAssetAsOrphanedCommandResult();
    }
}
