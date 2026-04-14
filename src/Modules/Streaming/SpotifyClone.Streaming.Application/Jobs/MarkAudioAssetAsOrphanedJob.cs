using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Streaming.Application.Features.Media.Commands.MarkAudioAssetAsOrphaned;

namespace SpotifyClone.Streaming.Application.Jobs;

public sealed class MarkAudioAssetAsOrphanedJob(
    ISender sender,
    ILogger<MarkAudioAssetAsOrphanedJob> logger)
{
    private readonly ISender _sender = sender;
    private readonly ILogger<MarkAudioAssetAsOrphanedJob> _logger = logger;

    public async Task ProcessAsync(
        Guid audioAssetId)
    {

        _logger.LogInformation(
            "Background job {Job} started.",
            typeof(MarkAudioAssetAsOrphanedJob).Name);

        await _sender.Send(new MarkAudioAssetAsOrphanedCommand(
            audioAssetId));
    }
}
