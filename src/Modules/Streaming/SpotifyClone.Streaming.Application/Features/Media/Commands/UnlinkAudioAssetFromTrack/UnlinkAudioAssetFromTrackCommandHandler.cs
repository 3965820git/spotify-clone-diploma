using Microsoft.Extensions.Logging;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Errors;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.ValueObjects;

namespace SpotifyClone.Streaming.Application.Features.Media.Commands.UnlinkAudioAssetFromTrack;

internal sealed class UnlinkAudioAssetFromTrackCommandHandler(
    IStreamingUnitOfWork unit,
    ILogger<UnlinkAudioAssetFromTrackCommandHandler> logger)
    : ICommandHandler<UnlinkAudioAssetFromTrackCommand, UnlinkAudioAssetFromTrackCommandResult>
{
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly ILogger<UnlinkAudioAssetFromTrackCommandHandler> _logger = logger;

    public async Task<Result<UnlinkAudioAssetFromTrackCommandResult>> Handle(
        UnlinkAudioAssetFromTrackCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Unlinking Audio Asset {AudioAssetId} from Track", request.AudioAssetId);

        AudioAsset? audioAsset = await _unit.AudioAssets.GetByIdAsync(
            AudioAssetId.From(request.AudioAssetId),
            cancellationToken);

        if (audioAsset is null)
        {
            _logger.LogWarning(
                "Audio Asset {AudioAssetId} not found while unlinking from Track",
                request.AudioAssetId);

            return Result.Failure<UnlinkAudioAssetFromTrackCommandResult>(MediaErrors.MediaAssetNotFound);
        }

        audioAsset.MarkAsOrphaned();

        return new UnlinkAudioAssetFromTrackCommandResult();
    }
}
