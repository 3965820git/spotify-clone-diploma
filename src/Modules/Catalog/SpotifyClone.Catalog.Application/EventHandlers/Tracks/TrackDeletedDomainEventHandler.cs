using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks.Events;
using SpotifyClone.Catalog.Domain.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;
using SpotifyClone.Shared.IntegrationEvents.Catalog.Tracks;

namespace SpotifyClone.Catalog.Application.EventHandlers.Tracks;

internal sealed class TrackDeletedDomainEventHandler(
    ICatalogUnitOfWork unit,
    AlbumTrackDomainService albumTrackDomainService,
    ILogger<TrackDeletedDomainEventHandler> logger)
    : INotificationHandler<TrackDeletedDomainEvent>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly AlbumTrackDomainService _albumTrackDomainService = albumTrackDomainService;
    private readonly ILogger<TrackDeletedDomainEventHandler> _logger = logger;

    public async Task Handle(
        TrackDeletedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.AlbumId is not null)
        {
            Album? album = await _unit.Albums.GetByIdAsync(
                notification.AlbumId, cancellationToken);

            if (album is null)
            {
                _logger.LogError(
                    "Album {AlbumId} not found while removing track {TrackId} from it",
                    notification.AlbumId.Value,
                    notification.TrackId.Value);
            }
            else
            {
                album.RemoveTrack(notification.TrackId);
                _albumTrackDomainService.TryMarkAlbumAsReadyToPublish(album);
                _albumTrackDomainService.ReevaluateAlbumType(album);
            }
        }

        var integrationEvent = new TrackDeletedIntegrationEvent(
            notification.TrackId.Value);
        var message = OutboxMessage.FromIntegrationEvent(integrationEvent);

        await _unit.OutboxMessages.AddAsync(message, cancellationToken);

        if (notification.AudioFileId is not null)
        {
            var integrationEvent2 = new TrackDeletedWithLinkedAudioIntegrationEvent(
                notification.TrackId.Value,
                notification.AudioFileId.Value);
            var message2 = OutboxMessage.FromIntegrationEvent(integrationEvent2);

            await _unit.OutboxMessages.AddAsync(message2, cancellationToken);
        }

        await _unit.CommitAsync(cancellationToken);
    }
}
