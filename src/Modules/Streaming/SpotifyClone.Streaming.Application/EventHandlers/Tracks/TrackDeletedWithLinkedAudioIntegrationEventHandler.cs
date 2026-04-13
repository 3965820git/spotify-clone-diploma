using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Services;
using SpotifyClone.Shared.IntegrationEvents.Catalog.Tracks;
using SpotifyClone.Streaming.Application.Jobs;

namespace SpotifyClone.Streaming.Application.EventHandlers.Tracks;

internal sealed class TrackDeletedWithLinkedAudioIntegrationEventHandler(
    IBackgroundJobService jobService,
    ILogger<TrackDeletedWithLinkedAudioIntegrationEventHandler> logger)
    : INotificationHandler<TrackDeletedWithLinkedAudioIntegrationEvent>
{
    private readonly IBackgroundJobService _jobService = jobService;
    private readonly ILogger<TrackDeletedWithLinkedAudioIntegrationEventHandler> _logger = logger;

    public async Task Handle(
        TrackDeletedWithLinkedAudioIntegrationEvent notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Starting background job {Job}...",
            typeof(MarkAudioAssetAsOrphanedJob).Name);

        _jobService.Enqueue<MarkAudioAssetAsOrphanedJob>(job => job.ProcessAsync(
            notification.AudioId));
    }
}
