using Microsoft.EntityFrameworkCore;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Moods.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks.ValueObjects;
using SpotifyClone.Catalog.Infrastructure.Persistence.Database;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Infrastructure.Persistence.Repositories;

internal sealed class TrackEfCoreRepository(CatalogAppDbContext context)
    : ITrackRepository
{
    private readonly DbSet<Track> _tracks = context.Tracks;

    public async Task<Track?> GetByIdAsync(
        TrackId id,
        CancellationToken cancellationToken = default)
        => await _tracks.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public async Task<IEnumerable<Track>> GetAllByAlbumAsync(
        AlbumId albumId,
        CancellationToken cancellationToken = default)
        => await _tracks
            .Where(a => a.AlbumId == albumId)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Track>> GetAllByGenreAsync(
        GenreId genreId,
        CancellationToken cancellationToken = default)
        => await _tracks
            .Where(a => a.Genres.Any(g => g.Value == genreId.Value))
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Track>> GetAllByMoodAsync(
        MoodId moodId,
        CancellationToken cancellationToken = default)
        => await _tracks
            .Where(a => a.Moods.Any(m => m.Value == moodId.Value))
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Track>> GetAllByMainArtistAsync(
        ArtistId albumId,
        CancellationToken cancellationToken = default)
        => await _tracks
            .Where(a => a.AlbumId == albumId)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Track>> GetByIdsAsync(
        IEnumerable<TrackId> ids,
        CancellationToken cancellationToken = default)
    {
        if (!ids.Any())
        {
            return [];
        }

        return await _tracks
            .Where(track => ids.Contains(track.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        Track track,
        CancellationToken cancellationToken = default)
        => await _tracks.AddAsync(track, cancellationToken);

    public async Task DeleteAsync(
        Track track,
        CancellationToken cancellationToken = default)
        => _tracks.Remove(track);

    public async Task<bool> IsAudioFileUsedAsync(
        AudioFileId audioFileId,
        CancellationToken cancellationToken)
        => await _tracks.AnyAsync(t => t.AudioFileId == audioFileId, cancellationToken);
}
