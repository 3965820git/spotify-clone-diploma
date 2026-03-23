using Microsoft.EntityFrameworkCore;
using SpotifyClone.Catalog.Application.Abstractions.Data;
using SpotifyClone.Catalog.Application.Features.Artists.Queries;
using SpotifyClone.Catalog.Application.Features.Genres.Queries;
using SpotifyClone.Catalog.Application.Features.Moods.Queries;
using SpotifyClone.Catalog.Application.Features.Tracks.Queries;
using SpotifyClone.Catalog.Application.Models;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Moods.ValueObjects;
using SpotifyClone.Catalog.Infrastructure.Persistence.Database;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Infrastructure.Persistence.Queries;

internal sealed class TrackEfCoreReadService(
    CatalogAppDbContext context)
    : ITrackReadService
{
    private readonly CatalogAppDbContext _context = context;

    public async Task<bool> ExistsAsync(
        TrackId id,
        CancellationToken cancellationToken = default)
        => await _context.Tracks.AnyAsync(t => t.Id == id, cancellationToken);

    public async Task<TrackDetails?> GetDetailsAsync(
        TrackId id,
        CancellationToken cancellationToken = default)
    {
        var trackInfo = await _context.Tracks
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(t => new
            {
                t.Id,
                t.Title,
                t.Duration,
                t.ReleaseDate,
                t.ContainsExplicitContent,
                t.Status,
                t.AudioFileId,
                t.AlbumId,
                MainArtistIds = t.MainArtists.ToList(),
                FeaturedArtistIds = t.FeaturedArtists.ToList(),
                GenreIds = t.Genres.ToList(),
                MoodIds = t.Moods.ToList()
            })
            .SingleOrDefaultAsync(cancellationToken);
        if (trackInfo == null)
        {
            return null;
        }

        List<ArtistSummary> mainArtists = await _context.Artists
            .AsNoTracking()
            .Where(a => trackInfo.MainArtistIds.Contains(a.Id))
            .Select(a => new ArtistSummary(
                a.Id.Value, a.Name,
                a.Status.Value,
                a.OwnerId == null ? null : a.OwnerId.Value,
                a.Avatar == null ? null : new ImageMetadataDetails(
                    a.Avatar.ImageId.Value,
                    a.Avatar.Metadata.Width,
                    a.Avatar.Metadata.Height,
                    a.Avatar.Metadata.FileType.Value,
                    a.Avatar.Metadata.SizeInBytes)))
            .ToListAsync(cancellationToken);

        List<ArtistSummary> featuredArtists = await _context.Artists
            .AsNoTracking()
            .Where(a => trackInfo.FeaturedArtistIds.Contains(a.Id))
            .Select(a => new ArtistSummary(
                a.Id.Value, a.Name,
                a.Status.Value,
                a.OwnerId == null ? null : a.OwnerId.Value,
                a.Avatar == null ? null : new ImageMetadataDetails(
                    a.Avatar.ImageId.Value,
                    a.Avatar.Metadata.Width,
                    a.Avatar.Metadata.Height,
                    a.Avatar.Metadata.FileType.Value,
                    a.Avatar.Metadata.SizeInBytes)))
            .ToListAsync(cancellationToken);

        List<GenreSummary> genres = await _context.Genres
            .AsNoTracking()
            .Where(g => trackInfo.GenreIds.Contains(g.Id))
            .Select(g => new GenreSummary(g.Id.Value, g.Name))
            .ToListAsync(cancellationToken);

        List<MoodSummary> moods = await _context.Moods
            .AsNoTracking()
            .Where(m => trackInfo.MoodIds.Contains(m.Id))
            .Select(m => new MoodSummary(m.Id.Value, m.Name))
            .ToListAsync(cancellationToken);

        return new TrackDetails(
            trackInfo.Id.Value,
            trackInfo.Title,
            trackInfo.Duration,
            trackInfo.ReleaseDate,
            trackInfo.ContainsExplicitContent,
            trackInfo.Status.Value,
            trackInfo.AudioFileId?.Value,
            trackInfo.AlbumId?.Value,
            mainArtists,
            featuredArtists,
            genres,
            moods
        );
    }

    public async Task<IEnumerable<TrackSummary>> GetAllByGenreIdAsync(
        GenreId genreId,
        CancellationToken cancellationToken = default)
        => await _context.Tracks
        .Where(t => t.Genres.Any(g => g.Value == genreId.Value))
        .Select(t => new TrackSummary(
            t.Id.Value,
            t.Title,
            t.Duration,
            t.ReleaseDate,
            t.ContainsExplicitContent,
            t.Status.Value,
            t.AudioFileId == null ? null : t.AudioFileId.Value,
            t.AlbumId == null ? null : t.AlbumId.Value))
        .ToListAsync(cancellationToken);

    public async Task<IEnumerable<TrackSummary>> GetAllByMoodIdAsync(
        MoodId moodId,
        CancellationToken cancellationToken = default)
        => await _context.Tracks
        .Where(t => t.Moods.Any(m => m.Value == moodId.Value))
        .Select(t => new TrackSummary(
            t.Id.Value,
            t.Title,
            t.Duration,
            t.ReleaseDate,
            t.ContainsExplicitContent,
            t.Status.Value,
            t.AudioFileId == null ? null : t.AudioFileId.Value,
            t.AlbumId == null ? null : t.AlbumId.Value))
        .ToListAsync(cancellationToken);
}
