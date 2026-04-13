using SpotifyClone.Catalog.Application.Abstractions.Repositories;
using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Aggregates.Genres;
using SpotifyClone.Catalog.Domain.Aggregates.Moods;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Abstractions;

public interface ICatalogUnitOfWork : IUnitOfWork
{
    ITrackRepository Tracks { get; }
    IAlbumRepository Albums { get; }
    IArtistRepository Artists { get; }
    IGenreRepository Genres { get; }
    IMoodRepository Moods { get; }
    IOutboxRepository OutboxMessages { get; }
}
