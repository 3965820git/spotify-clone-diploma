using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks.Events;
using SpotifyClone.Catalog.Domain.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;
using SpotifyClone.Shared.IntegrationEvents.Catalog.Tracks;

namespace SpotifyClone.Catalog.Application.EventHandlers.Tracks;

internal sealed class TrackUnlinkedFromAudioFileDomainEventHandler(
    ICatalogUnitOfWork unit,
    AlbumTrackDomainService albumTrackDomainService,
    ILogger<TrackUnlinkedFromAudioFileDomainEventHandler> logger)
    : INotificationHandler<TrackUnlinkedFromAudioFileDomainEvent>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly AlbumTrackDomainService _albumTrackDomainService = albumTrackDomainService;
    private readonly ILogger<TrackUnlinkedFromAudioFileDomainEventHandler> _logger = logger;

    public async Task Handle(
        TrackUnlinkedFromAudioFileDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.AlbumId is not null)
        {
            Album? album = await _unit.Albums.GetByIdAsync(
                notification.AlbumId, cancellationToken);

            if (album is null)
            {
                _logger.LogError(
                    "Album {Id} not found while reevaluateing it's type and status",
                    notification.AlbumId);
            }
            else
            {
                _albumTrackDomainService.TryMarkAlbumAsReadyToPublish(album);
                _albumTrackDomainService.ReevaluateAlbumType(album);
            }
        }

        var integrationEvent = new TrackUnlinkedFromAudioIntegrationEvent(
                notification.AudioFileId.Value);

        var message = OutboxMessage.FromIntegrationEvent(integrationEvent);

        await _unit.OutboxMessages.AddAsync(message, cancellationToken);
        await _unit.CommitAsync(cancellationToken);
    }
}
