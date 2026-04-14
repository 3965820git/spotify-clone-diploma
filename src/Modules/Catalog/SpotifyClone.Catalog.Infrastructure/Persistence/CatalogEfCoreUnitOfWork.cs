using MediatR;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Abstractions.Repositories;
using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Aggregates.Genres;
using SpotifyClone.Catalog.Domain.Aggregates.Moods;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;
using SpotifyClone.Catalog.Infrastructure.Persistence.Database;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence;

namespace SpotifyClone.Catalog.Infrastructure.Persistence;

internal sealed class CatalogEfCoreUnitOfWork(
    CatalogAppDbContext context,
    ITrackRepository tracks,
    IAlbumRepository albums,
    IArtistRepository artists,
    IGenreRepository genres,
    IMoodRepository moods,
    IOutboxRepository outbox,
    IPublisher publisher)
    : EfCoreUnitOfWorkBase<CatalogAppDbContext>(context, publisher),
    ICatalogUnitOfWork
{
    public ITrackRepository Tracks => tracks;
    public IAlbumRepository Albums => albums;
    public IArtistRepository Artists => artists;
    public IGenreRepository Genres => genres;
    public IMoodRepository Moods => moods;
    public IOutboxRepository OutboxMessages => outbox;
}
