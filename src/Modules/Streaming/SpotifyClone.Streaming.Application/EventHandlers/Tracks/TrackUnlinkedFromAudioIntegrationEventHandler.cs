using MediatR;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Services;
using SpotifyClone.Shared.IntegrationEvents.Catalog.Tracks;
using SpotifyClone.Streaming.Application.Jobs;

namespace SpotifyClone.Streaming.Application.EventHandlers.Tracks;

internal sealed class TrackUnlinkedFromAudioIntegrationEventHandler(
    IBackgroundJobService jobService)
    : INotificationHandler<TrackUnlinkedFromAudioIntegrationEvent>
{
    private readonly IBackgroundJobService _jobService = jobService;

    public async Task Handle(
        TrackUnlinkedFromAudioIntegrationEvent notification,
        CancellationToken cancellationToken)
        => _jobService.Enqueue<UnlinkAudioAssetFromTrackJob>(job =>
        job.ProcessAsync(notification.AudioId, cancellationToken));
}
