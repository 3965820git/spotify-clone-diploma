using MediatR;
using SpotifyClone.Shared.IntegrationEvents.Streaming.AudioAssets;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Events;

namespace SpotifyClone.Streaming.Application.EventHandlers.AudioAssets;

internal sealed class AudioAssetCreatedDomainEventHandler(
    IPublisher publisher)
    : INotificationHandler<AudioAssetCreatedDomainEvent>
{
    private readonly IPublisher _publisher = publisher;

    public async Task Handle(
        AudioAssetCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var integrationEvent = new AudioUploadStartedIntegrationEvent(
            notification.AudioAssetId.Value,
            notification.TrackId.Value,
            notification.Duration
        );

        await _publisher.Publish(integrationEvent, cancellationToken);
    }
}
