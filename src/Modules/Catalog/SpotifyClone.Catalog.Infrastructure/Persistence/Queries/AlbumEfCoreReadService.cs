using Microsoft.EntityFrameworkCore;
using SpotifyClone.Catalog.Application.Abstractions.Data;
using SpotifyClone.Catalog.Application.Features.Albums.Queries;
using SpotifyClone.Catalog.Application.Features.Artists.Queries;
using SpotifyClone.Catalog.Application.Features.Tracks.Queries;
using SpotifyClone.Catalog.Application.Models;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.Enums;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Catalog.Infrastructure.Persistence.Database;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Infrastructure.Persistence.Queries;

internal sealed class AlbumEfCoreReadService(
    CatalogAppDbContext context)
    : IAlbumReadService
{
    private readonly CatalogAppDbContext _context = context;

    public async Task<AlbumDetails?> GetDetailsAsync(
        AlbumId id,
        CancellationToken cancellationToken = default)
    {
        // Крок 1. Дістаємо "корінь" (Альбом) та його внутрішні колекції ID-шників.
        var albumBase = await _context.Albums
            .AsNoTracking()
            .Where(a => a.Id == id)
            .Include(a => a.Tracks)
            .Select(a => new
            {
                a.Id,
                a.Title,
                a.ReleaseDate,
                a.Status,
                a.Type,
                a.Cover,
                ArtistIds = a.MainArtists,
                AlbumTracks = a.Tracks
            })
            .SingleOrDefaultAsync(cancellationToken);
        if (albumBase == null)
        {
            return null;
        }

        // Крок 2. Дістаємо Артистів окремим запитом.
        var artistIds = albumBase.ArtistIds.ToList();

        List<ArtistSummary> artists = await _context.Artists
            .Where(artist => artistIds.Contains(artist.Id))
            .Select(artist => new ArtistSummary(
                artist.Id.Value,
                artist.Name,
                artist.Status.Value,
                artist.OwnerId == null ? null : artist.OwnerId.Value,
                artist.Avatar == null ? null : new ImageMetadataDetails(
                    artist.Avatar.ImageId.Value,
                    artist.Avatar.Metadata.Width,
                    artist.Avatar.Metadata.Height,
                    artist.Avatar.Metadata.FileType.Value,
                    artist.Avatar.Metadata.SizeInBytes)
            ))
            .ToListAsync(cancellationToken);

        // Крок 3. Дістаємо Треки окремим запитом.
        var trackIds = albumBase.AlbumTracks.Select(at => at.Id).ToList();

        var tracksDb = await _context.Tracks
            .Where(t => trackIds.Contains(t.Id))
            .Select(t => new
            {
                t.Id,
                t.Title,
                t.ContainsExplicitContent,
                t.Status,
                t.Duration
            })
            .ToListAsync(cancellationToken);

        // Крок 4. Мапимо позиції з AlbumTracks на отримані дані Треків (In-Memory Join)
        var trackSummaries = tracksDb.Select(t =>
        {
            // Знаходимо позицію для цього конкретного треку з даних першого запиту
            int position = albumBase.AlbumTracks.First(at => at.Id == t.Id).Position;

            return new AlbumTrackSummary(
                t.Id.Value,
                t.Title,
                t.ContainsExplicitContent,
                t.Status.Value,
                t.Duration,
                position
            );
        })
        .OrderBy(t => t.Position) // Сортуємо вже в пам'яті сервера
        .ToList();

        // Крок 5. Збираємо фінальний DTO
        return new AlbumDetails(
            albumBase.Id.Value,
            albumBase.Title,
            albumBase.ReleaseDate,
            albumBase.Status.Value,
            albumBase.Type.Value,
            albumBase.Cover == null ? null : new ImageMetadataDetails(
                albumBase.Cover.ImageId.Value,
                albumBase.Cover.Metadata.Width,
                albumBase.Cover.Metadata.Height,
                albumBase.Cover.Metadata.FileType.Value,
                albumBase.Cover.Metadata.SizeInBytes),
            artists,
            trackSummaries
        );
    }

    public async Task<AlbumDetails?> GetDetailsByTrackIdAsync(
        TrackId trackId,
        CancellationToken cancellationToken = default)
    {
        // Крок 1. Дістаємо "корінь" (Альбом) та його внутрішні колекції ID-шників.
        var albumBase = await _context.Albums
            .AsNoTracking()
            .Where(a => a.Tracks.Any(t => t.Id.Value == trackId.Value))
            .Include(a => a.Tracks)
            .Select(a => new
            {
                a.Id,
                a.Title,
                a.ReleaseDate,
                a.Status,
                a.Type,
                a.Cover,
                ArtistIds = a.MainArtists,
                AlbumTracks = a.Tracks
            })
            .SingleOrDefaultAsync(cancellationToken);
        if (albumBase == null)
        {
            return null;
        }

        // Крок 2. Дістаємо Артистів окремим запитом.
        var artistIds = albumBase.ArtistIds.ToList();

        List<ArtistSummary> artists = await _context.Artists
            .Where(artist => artistIds.Contains(artist.Id))
            .Select(artist => new ArtistSummary(
                artist.Id.Value,
                artist.Name,
                artist.Status.Value,
                artist.OwnerId == null ? null : artist.OwnerId.Value,
                artist.Avatar == null ? null : new ImageMetadataDetails(
                    artist.Avatar.ImageId.Value,
                    artist.Avatar.Metadata.Width,
                    artist.Avatar.Metadata.Height,
                    artist.Avatar.Metadata.FileType.Value,
                    artist.Avatar.Metadata.SizeInBytes)
            ))
            .ToListAsync(cancellationToken);

        // Крок 3. Дістаємо Треки окремим запитом.
        var trackIds = albumBase.AlbumTracks.Select(at => at.Id).ToList();

        var tracksDb = await _context.Tracks
            .Where(t => trackIds.Contains(t.Id))
            .Select(t => new
            {
                t.Id,
                t.Title,
                t.ContainsExplicitContent,
                t.Status,
                t.Duration
            })
            .ToListAsync(cancellationToken);

        // Крок 4. Мапимо позиції з AlbumTracks на отримані дані Треків (In-Memory Join)
        var trackSummaries = tracksDb.Select(t =>
        {
            // Знаходимо позицію для цього конкретного треку з даних першого запиту
            int position = albumBase.AlbumTracks.First(at => at.Id == t.Id).Position;

            return new AlbumTrackSummary(
                t.Id.Value,
                t.Title,
                t.ContainsExplicitContent,
                t.Status.Value,
                t.Duration,
                position
            );
        })
        .OrderBy(t => t.Position) // Сортуємо вже в пам'яті сервера
        .ToList();

        // Крок 5. Збираємо фінальний DTO
        return new AlbumDetails(
            albumBase.Id.Value,
            albumBase.Title,
            albumBase.ReleaseDate,
            albumBase.Status.Value,
            albumBase.Type.Value,
            albumBase.Cover == null ? null : new ImageMetadataDetails(
                albumBase.Cover.ImageId.Value,
                albumBase.Cover.Metadata.Width,
                albumBase.Cover.Metadata.Height,
                albumBase.Cover.Metadata.FileType.Value,
                albumBase.Cover.Metadata.SizeInBytes),
            artists,
            trackSummaries
        );
    }

    public async Task<AlbumSummary?> GetSummary(
        AlbumId id,
        CancellationToken cancellationToken = default)
        => await _context.Albums
        .AsNoTracking()
        .Where(a => a.Id == id)
        .Select(a => new AlbumSummary(
            a.Id.Value,
            a.Title,
            a.ReleaseDate,
            a.Status.Value,
            a.Type.Value,
            a.Cover == null ? null : new ImageMetadataDetails(
                a.Cover.ImageId.Value,
                a.Cover.Metadata.Width,
                a.Cover.Metadata.Height,
                a.Cover.Metadata.FileType.Value,
                a.Cover.Metadata.SizeInBytes),
            _context.Artists
            .Where(art => a.MainArtists.Select(ma => ma.Value).Contains(art.Id.Value))
            .Select(art => new ArtistSummary(
                art.Id.Value,
                art.Name,
                art.Status.Value,
                art.OwnerId == null ? null : art.OwnerId.Value,
                art.Avatar == null ? null : new ImageMetadataDetails(
                    art.Avatar.ImageId.Value,
                    art.Avatar.Metadata.Width,
                    art.Avatar.Metadata.Height,
                    art.Avatar.Metadata.FileType.Value,
                    art.Avatar.Metadata.SizeInBytes)))
            .ToList()))
        .SingleOrDefaultAsync(cancellationToken);

    public async Task<AlbumSummary?> GetSummaryByTrackId(
        TrackId trackId,
        CancellationToken cancellationToken = default)
        => await _context.Albums
        .AsNoTracking()
        .Where(a => a.Tracks.Any(t => t.Id.Value == trackId.Value))
        .Select(a => new AlbumSummary(
            a.Id.Value,
            a.Title,
            a.ReleaseDate,
            a.Status.Value,
            a.Type.Value,
            a.Cover == null ? null : new ImageMetadataDetails(
                a.Cover.ImageId.Value,
                a.Cover.Metadata.Width,
                a.Cover.Metadata.Height,
                a.Cover.Metadata.FileType.Value,
                a.Cover.Metadata.SizeInBytes),
            _context.Artists
            .Where(art => a.MainArtists.Select(ma => ma.Value).Contains(art.Id.Value))
            .Select(art => new ArtistSummary(
                art.Id.Value,
                art.Name,
                art.Status.Value,
                art.OwnerId == null ? null : art.OwnerId.Value,
                art.Avatar == null ? null : new ImageMetadataDetails(
                    art.Avatar.ImageId.Value,
                    art.Avatar.Metadata.Width,
                    art.Avatar.Metadata.Height,
                    art.Avatar.Metadata.FileType.Value,
                    art.Avatar.Metadata.SizeInBytes)))
            .ToList()))
        .SingleOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<AlbumSummary>> GetAllByArtistIdAsync(
        ArtistId artistId,
        CancellationToken cancellationToken = default)
        => await _context.Albums
        .Where(a => a.MainArtists.Any(a => a.Value == artistId.Value))
        .Select(a => new AlbumSummary(
            a.Id.Value,
            a.Title,
            a.ReleaseDate,
            a.Status.Value,
            a.Type.Value,
            a.Cover == null ? null : new ImageMetadataDetails(
                a.Cover.ImageId.Value,
                a.Cover.Metadata.Width,
                a.Cover.Metadata.Height,
                a.Cover.Metadata.FileType.Value,
                a.Cover.Metadata.SizeInBytes),
            _context.Artists
            .Where(art => a.MainArtists.Select(ma => ma.Value).Contains(art.Id.Value))
            .Select(art => new ArtistSummary(
                art.Id.Value,
                art.Name,
                art.Status.Value,
                art.OwnerId == null ? null : art.OwnerId.Value,
                art.Avatar == null ? null : new ImageMetadataDetails(
                    art.Avatar.ImageId.Value,
                    art.Avatar.Metadata.Width,
                    art.Avatar.Metadata.Height,
                    art.Avatar.Metadata.FileType.Value,
                    art.Avatar.Metadata.SizeInBytes)))
            .ToList()))
        .ToListAsync(cancellationToken);

    public async Task<IEnumerable<AlbumSummary>> GetAllPublishedByArtistIdAsync(
        ArtistId artistId,
        CancellationToken cancellationToken = default)
        => await _context.Albums
        .Where(a =>
            a.Status.Value == AlbumStatus.Published.Value &&
            a.MainArtists.Any(a => a.Value == artistId.Value))
        .Select(a => new AlbumSummary(
            a.Id.Value,
            a.Title,
            a.ReleaseDate,
            a.Status.Value,
            a.Type.Value,
            a.Cover == null ? null : new ImageMetadataDetails(
                a.Cover.ImageId.Value,
                a.Cover.Metadata.Width,
                a.Cover.Metadata.Height,
                a.Cover.Metadata.FileType.Value,
                a.Cover.Metadata.SizeInBytes),
            _context.Artists
            .Where(art => a.MainArtists.Select(ma => ma.Value).Contains(art.Id.Value))
            .Select(art => new ArtistSummary(
                art.Id.Value,
                art.Name,
                art.Status.Value,
                art.OwnerId == null ? null : art.OwnerId.Value,
                art.Avatar == null ? null : new ImageMetadataDetails(
                    art.Avatar.ImageId.Value,
                    art.Avatar.Metadata.Width,
                    art.Avatar.Metadata.Height,
                    art.Avatar.Metadata.FileType.Value,
                    art.Avatar.Metadata.SizeInBytes)))
            .ToList()))
        .ToListAsync(cancellationToken);
}
