using MediatR;
using SpotifyClone.Catalog.Application.Jobs;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Services;
using SpotifyClone.Shared.IntegrationEvents.Streaming.AudioAssets;

namespace SpotifyClone.Catalog.Application.EventHandlers.Audio;

internal sealed class UnlinkAudioFromTrackFailedIntegrationEventHandler(
    IBackgroundJobService jobService)
    : INotificationHandler<UnlinkAudioFromTrackFailedIntegrationEvent>
{
    private readonly IBackgroundJobService _jobService = jobService;

    public async Task Handle(
        UnlinkAudioFromTrackFailedIntegrationEvent notification,
        CancellationToken cancellationToken)
        => _jobService.Enqueue<LinkTrackToAudioFileJob>(job =>
        job.ProcessAsync(
            notification.TrackId,
            notification.AudioId,
            notification.Duration));
}
