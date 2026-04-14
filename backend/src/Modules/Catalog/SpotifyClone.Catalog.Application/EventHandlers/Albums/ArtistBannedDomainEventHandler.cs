using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.Events;

namespace SpotifyClone.Catalog.Application.EventHandlers.Albums;

internal sealed class ArtistBannedDomainEventHandler(
    ICatalogUnitOfWork unit,
    ILogger<ArtistBannedDomainEventHandler> logger)
    : INotificationHandler<ArtistBannedDomainEvent>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ILogger<ArtistBannedDomainEventHandler> _logger = logger;

    public async Task Handle(
        ArtistBannedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        Artist? artist = await _unit.Artists.GetBannedByIdAsync(
            notification.ArtistId, cancellationToken);
        if (artist is null)
        {
            _logger.LogError(
                "Artist with ID {ArtistId} was not found while trying to soft delete it.",
                notification.ArtistId);
            
            throw new InvalidOperationException($"Artist {notification.ArtistId} not found.");
        }

        IEnumerable<Album> albums = await _unit.Albums.GetAllByMainArtistAsync(
            artist.Id, cancellationToken);

        foreach (Album album in albums)
        {
            album.RemoveMainArtist(artist.Id);
        }

        await _unit.CommitAsync(cancellationToken);
    }
}
