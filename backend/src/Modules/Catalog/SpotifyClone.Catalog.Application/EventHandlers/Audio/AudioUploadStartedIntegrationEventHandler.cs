using MediatR;
using SpotifyClone.Catalog.Application.Jobs;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Services;
using SpotifyClone.Shared.IntegrationEvents.Streaming.AudioAssets;

namespace SpotifyClone.Catalog.Application.EventHandlers.Audio;

internal sealed class AudioUploadStartedIntegrationEventHandler(
    IBackgroundJobService jobService)
    : INotificationHandler<AudioUploadStartedIntegrationEvent>
{
    private readonly IBackgroundJobService _jobService = jobService;

    public async Task Handle(AudioUploadStartedIntegrationEvent notification, CancellationToken cancellationToken)
        => _jobService.Enqueue<LinkTrackToAudioFileJob>(job =>
            job.ProcessAsync(
                notification.TrackId,
                notification.AudioId,
                notification.Duration));
}
