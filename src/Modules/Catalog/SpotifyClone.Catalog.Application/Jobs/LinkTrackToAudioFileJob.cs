using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Features.Tracks.Commands.LinkToAudioFile;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.IntegrationEvents.Catalog.Tracks;

namespace SpotifyClone.Catalog.Application.Jobs;

public sealed class LinkTrackToAudioFileJob(
    ISender sender,
    ICatalogUnitOfWork unit,
    IPublisher publisher,
    ILogger<LinkTrackToAudioFileJob> logger)
{
    private readonly ISender _sender = sender;
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly IPublisher _publisher = publisher;
    private readonly ILogger<LinkTrackToAudioFileJob> _logger = logger;

    public async Task ProcessAsync(
        Guid trackId,
        Guid audioFileId,
        TimeSpan duration,
        CancellationToken cancellationToken = default)
    {
        Result<LinkTrackToAudioFileCommandResult> result = await _sender.Send(
            new LinkTrackToAudioFileCommand(trackId, audioFileId, duration),
            cancellationToken);

        if (result.IsFailure)
        {
            string errors = string.Join("\n", result.Errors.Select(e => $"{e.Code}: {e.Description}"));

            _logger.LogError(
                "LinkTrackToAudioFileJob failed for Track {TrackId}. Rolling back...", trackId);

            await _publisher.Publish(
                new LinkTrackToAudioFailedIntegrationEvent(audioFileId),
                cancellationToken);

            throw new InvalidOperationException(
                $"LinkTrackToAudioFileJob failed for Track {trackId}. Errors:\n{errors}");
        }

        await _unit.CommitAsync(cancellationToken);
    }
}
