using SpotifyClone.Shared.Kernel.Contracts.Catalog;

namespace SpotifyClone.Search.Application.Abstractions.Clients;

public interface ICatalogModuleClient
{
    Task<IEnumerable<GenreSharedDto>> GetAllGenresAsync(
        CancellationToken cancellationToken = default);

    Task<IEnumerable<MoodSharedDto>> GetAllMoodsAsync(
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ArtistSharedDto>> GetAllArtistsAsync(
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TrackSharedDto>> GetAllTracksAsync(
        CancellationToken cancellationToken = default);

    Task<IEnumerable<AlbumSharedDto>> GetAllAlbumsAsync(
        CancellationToken cancellationToken = default);
}
