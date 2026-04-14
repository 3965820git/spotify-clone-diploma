using MediatR;
using SpotifyClone.Catalog.Application.Jobs;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Services;
using SpotifyClone.Shared.IntegrationEvents.Streaming.AudioAssets;

namespace SpotifyClone.Catalog.Application.EventHandlers.Audio;

internal sealed class AudioUploadedIntegrationEventHandler(
    IBackgroundJobService jobService)
    : INotificationHandler<AudioUploadedIntegrationEvent>
{
    private readonly IBackgroundJobService _jobService = jobService;

    public async Task Handle(AudioUploadedIntegrationEvent notification, CancellationToken cancellationToken)
        => _jobService.Enqueue<MarkTrackAsReadyToPublishJob>(job =>
            job.ProcessAsync(notification.TrackId));
}
