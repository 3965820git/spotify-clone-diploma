using MediatR;
using SpotifyClone.Shared.IntegrationEvents.Streaming.AudioAssets;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Events;

namespace SpotifyClone.Streaming.Application.EventHandlers.AudioAssets;

internal sealed class AudioAssetUploadedDomainEventHandler(
    IPublisher publisher)
    : INotificationHandler<AudioAssetUploadedDomainEvent>
{
    private readonly IPublisher _publisher = publisher;

    public async Task Handle(
        AudioAssetUploadedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var integrationEvent = new AudioUploadedIntegrationEvent(
            notification.TrackId.Value
        );

        await _publisher.Publish(integrationEvent, cancellationToken);
    }
}
