using Microsoft.EntityFrameworkCore;
using SpotifyClone.Catalog.Application.Abstractions.Data;
using SpotifyClone.Catalog.Application.Features.Artists.Queries;
using SpotifyClone.Catalog.Application.Features.Genres.Queries;
using SpotifyClone.Catalog.Application.Features.Moods.Queries;
using SpotifyClone.Catalog.Application.Features.Tracks.Queries;
using SpotifyClone.Catalog.Application.Models;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks.Enums;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks.ValueObjects;
using SpotifyClone.Catalog.Infrastructure.Persistence.Database;
using SpotifyClone.Shared.BuildingBlocks.Application.Pagination;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence.Extensions;
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

    public async Task<TrackSummary?> GetSummaryAsync(
        TrackId id,
        CancellationToken cancellationToken = default)
        => await _context.Tracks
        .AsNoTracking()
        .Where(t => t.Id == id)
        .Select(t => new TrackSummary(
            t.Id.Value,
            t.Title,
            t.Duration,
            t.ReleaseDate,
            t.ContainsExplicitContent,
            t.Status.Value,
            t.AudioFileId == null ? null : t.AudioFileId.Value,
            t.AlbumId == null ? null : t.AlbumId.Value,
            t.MainArtists.Select(a => a.Value),
            t.FeaturedArtists.Select(a => a.Value)))
        .SingleOrDefaultAsync(cancellationToken);

    public async Task<PagedList<TrackSummary>> GetAllAsync(
    UserId? ownerId,
    bool isAdmin,
    TrackFilterParams filters,
    PaginationParams pagination,
    CancellationToken cancellationToken = default)
    {
        IQueryable<Track> query = _context.Tracks.AsNoTracking();

        if (!isAdmin)
        {
            if (ownerId is null)
            {
                query = query.Where(t => t.Status == TrackStatus.Published);
            }
            else
            {
                // Отримуємо ID артистів користувача як об'єкти типу ArtistId
                List<ArtistId> userArtistIds = await _context.Artists
                    .Where(art => art.OwnerId == ownerId)
                    .Select(art => art.Id)
                    .ToListAsync(cancellationToken);

                // 2. Фільтруємо: трек опубліковано АБО 
                // користувач володіє хоча б одним з основних артистів АБО 
                // користувач володіє хоча б одним із запрошених артистів
                query = query.Where(t =>
                    t.Status == TrackStatus.Published ||
                    t.MainArtists.Any(ma => userArtistIds.Contains(ma)) ||
                    t.FeaturedArtists.Any(fa => userArtistIds.Contains(fa))
                );
            }
        }

        if (filters.Title is not null)
        {
            query = query.Where(t => EF.Functions.ILike(t.Title, filters.Title));
        }
        if (filters.Duration is not null)
        {
            query = query.Where(t => t.Duration == filters.Duration);
        }
        if (filters.ReleaseDate is not null)
        {
            query = query.Where(t => t.ReleaseDate == filters.ReleaseDate);
        }
        if (filters.Explicit is not null)
        {
            query = query.Where(t => t.ContainsExplicitContent == filters.Explicit);
        }
        if (filters.Status is not null)
        {
            var status = TrackStatus.From(filters.Status);
            query = query.Where(t => t.Status == status);
        }
        if (filters.AudioFileId is not null)
        {
            var audioFileId = AudioFileId.From(filters.AudioFileId.Value);
            query = query.Where(t => t.AudioFileId == audioFileId);
        }
        if (filters.AlbumId is not null)
        {
            var albumId = AlbumId.From(filters.AlbumId.Value);
            query = query.Where(t => t.AlbumId == albumId);
        }
        if (filters.MainArtistIds is not null && filters.MainArtistIds.Any())
        {
            query = query.Where(t => t.MainArtists.Any(a => filters.MainArtistIds.Any(id => id == a.Value)));
        }
        if (filters.FeaturedArtistIds is not null && filters.FeaturedArtistIds.Any())
        {
            query = query.Where(t => t.FeaturedArtists.Any(a => filters.FeaturedArtistIds.Any(id => id == a.Value)));
        }
        if (filters.GenreIds is not null && filters.GenreIds.Any())
        {
            query = query.Where(t => t.Genres.Any(a => filters.GenreIds.Any(id => id == a.Value)));
        }
        if (filters.MoodIds is not null && filters.MoodIds.Any())
        {
            query = query.Where(t => t.Moods.Any(a => filters.MoodIds.Any(id => id == a.Value)));
        }

        return await query
            // Додано сортування, щоб ToPagedListAsync працював коректно 
            .OrderBy(t => t.CreatedAtUtc)
            .Select(t => new TrackSummary(
                t.Id.Value,
                t.Title,
                t.Duration,
                t.ReleaseDate,
                t.ContainsExplicitContent,
                t.Status.Value,
                t.AudioFileId == null ? null : t.AudioFileId.Value,
                t.AlbumId == null ? null : t.AlbumId.Value,
                t.MainArtists.Select(a => a.Value),
                t.FeaturedArtists.Select(a => a.Value)))
            .ToPagedListAsync(pagination, cancellationToken);
    }

    public async Task<IEnumerable<TrackSummary>> GetAllByIdsAsync(
        IEnumerable<TrackId> ids,
        CancellationToken cancellationToken = default)
        => await _context.Tracks
        .AsNoTracking()
        .Where(t => ids.Any(id => id == t.Id))
        .Select(t => new TrackSummary(
            t.Id.Value,
            t.Title,
            t.Duration,
            t.ReleaseDate,
            t.ContainsExplicitContent,
            t.Status.Value,
            t.AudioFileId == null ? null : t.AudioFileId.Value,
            t.AlbumId == null ? null : t.AlbumId.Value,
            t.MainArtists.Select(a => a.Value),
            t.FeaturedArtists.Select(a => a.Value)))
        .ToListAsync(cancellationToken);
}
