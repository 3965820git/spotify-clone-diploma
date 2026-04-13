using SpotifyClone.Catalog.Domain.Aggregates.Albums.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;

namespace SpotifyClone.Catalog.Domain.Aggregates.Albums;

public interface IAlbumRepository
{
    Task<Album?> GetByIdAsync(
        AlbumId id,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Album>> GetAllByMainArtistAsync(
        ArtistId artistId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Album album,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Album album,
        CancellationToken cancellationToken = default);
}
