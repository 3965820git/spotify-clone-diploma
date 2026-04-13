using SpotifyClone.Catalog.Domain.Aggregates.Albums.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Moods.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks.ValueObjects;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Domain.Aggregates.Tracks;

public interface ITrackRepository
{
    Task<Track?> GetByIdAsync(
        TrackId id,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Track>> GetAllByAlbumAsync(
        AlbumId albumId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Track>> GetAllByGenreAsync(
        GenreId genreId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Track>> GetAllByMoodAsync(
        MoodId moodId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Track>> GetAllByMainArtistAsync(
        ArtistId albumId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Track>> GetByIdsAsync(
        IEnumerable<TrackId> ids,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Track track,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Track track,
        CancellationToken cancellationToken = default);

    Task<bool> IsAudioFileUsedAsync(AudioFileId audioFileId, CancellationToken cancellationToken);
}
