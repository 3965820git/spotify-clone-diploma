using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Features.Media.Commands.UnlinkAudioAssetFromTrack;

namespace SpotifyClone.Streaming.Application.Jobs;

internal sealed class UnlinkAudioAssetFromTrackJob(
    ISender sender,
    IStreamingUnitOfWork unit,
    ILogger<UnlinkAudioAssetFromTrackJob> logger)
{
    private readonly ISender _sender = sender;
    private readonly IStreamingUnitOfWork _unit = unit;
    private readonly ILogger<UnlinkAudioAssetFromTrackJob> _logger = logger;

    public async Task ProcessAsync(
        Guid audioAssetId,
        CancellationToken cancellationToken = default)
    {
        Result<UnlinkAudioAssetFromTrackCommandResult> result = await _sender.Send(
            new UnlinkAudioAssetFromTrackCommand(audioAssetId),
            cancellationToken);

        if (result.IsFailure)
        {
            string errors = string.Join("\n", result.Errors.Select(e => $"{e.Code}: {e.Description}"));

            _logger.LogError(
                "UnlinkAudioAssetFromTrackJob failed for Audio Asset {AudioAssetId}. Rolling back...",
                audioAssetId);

            throw new InvalidOperationException(
                $"UnlinkAudioAssetFromTrackJob failed for Audio Asset {audioAssetId}. Errors:\n{errors}");
        }

        await _unit.CommitAsync(cancellationToken);
    }
}
