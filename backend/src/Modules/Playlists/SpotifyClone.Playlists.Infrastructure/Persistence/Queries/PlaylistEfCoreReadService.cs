using Microsoft.EntityFrameworkCore;
using SpotifyClone.Playlists.Application.Abstractions.Data;
using SpotifyClone.Playlists.Application.Features.Playlists.Queries;
using SpotifyClone.Playlists.Application.Models;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.Entities;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;
using SpotifyClone.Playlists.Infrastructure.Persistence.Database;
using SpotifyClone.Playlists.Infrastructure.Persistence.Entities;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Infrastructure.Persistence.Queries;

internal sealed class PlaylistEfCoreReadService(
    PlaylistsAppDbContext context)
    : IPlaylistReadService
{
    private readonly PlaylistsAppDbContext _context = context;

    public async Task<PlaylistDetails?> GetDetailsAsync(
    PlaylistId id,
    CancellationToken cancellationToken = default)
    {
        var header = await _context.Playlists
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.OwnerId,
                p.IsPublic,
                p.Cover,
                TrackInfos = p.Tracks.OrderBy(t => t.Position)
                    .Select(t => new { t.Id, t.Position }).ToList(),
                CollaboratorIds = p.Collaborators.Select(c => c.Value).ToList()
            })
            .SingleOrDefaultAsync(cancellationToken);
        if (header == null)
        {
            return null;
        }

        var top4Ids = header.TrackInfos.Take(4).Select(x => x.Id.Value).ToList();

        List<CollaboratorSummary> collaboratorsTask = await _context.UserReferences
            .AsNoTracking()
            .Where(u => header.CollaboratorIds.Contains(u.Id))
            .Select(u => new CollaboratorSummary(u.Id, u.Name, u.AvatarImageId))
            .ToListAsync(cancellationToken);

        List<Guid> validCoverIdsTask = await _context.TrackReferences
            .AsNoTracking()
            .Where(t => top4Ids.Contains(t.Id))
            .Select(t => t.Id)
            .ToListAsync(cancellationToken);

        return new PlaylistDetails(
            header.Id.Value,
            header.Name,
            header.Description,
            header.OwnerId.Value,
            header.IsPublic,
            header.Cover == null ? null : new ImageMetadataDetails(
                header.Cover.ImageId.Value,
                header.Cover.Metadata.Width,
                header.Cover.Metadata.Height,
                header.Cover.Metadata.FileType.Value,
                header.Cover.Metadata.SizeInBytes),
            validCoverIdsTask
                .OrderBy(id => header.TrackInfos.FindIndex(x => x.Id.Value == id))
                .ToList(),
            collaboratorsTask,
            header.TrackInfos.Select(t => new PlaylistTrackSummary(t.Id.Value, t.Position)).ToList()
        );
    }

    public async Task<IEnumerable<PlaylistSummary>> GetAllByOwnerAsync(
    UserId ownerId,
    CancellationToken cancellationToken = default)
    => await GetPlaylistSummaries(
        _context.Playlists.Where(p => p.OwnerId == ownerId),
        cancellationToken);

    public async Task<IEnumerable<PlaylistSummary>> GetAllPublicByOwnerAsync(
        UserId ownerId,
        CancellationToken cancellationToken = default)
        => await GetPlaylistSummaries(
            _context.Playlists.Where(p => p.OwnerId == ownerId && p.IsPublic),
            cancellationToken);

    private async Task<List<PlaylistSummary>> GetPlaylistSummaries(
    IQueryable<Playlist> query,
    CancellationToken ct)
    {
        var playlists = await query
            .AsNoTracking()
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.IsPublic,
                p.Cover
            })
            .ToListAsync(ct);

        var playlistIds = playlists.Select(p => p.Id).ToList();

        List<PlaylistTrack> playlistTracks = await _context.PlaylistTracks
            .Where(pt => playlistIds.Contains(pt.PlaylistId))
            .AsNoTracking()
            .ToListAsync(ct);

        List<TrackReference> trackRefs = await _context.TrackReferences
            .AsNoTracking()
            .ToListAsync(ct);

        var trackLookup = playlistTracks
            .Join(trackRefs,
                pt => pt.Id.Value,
                tr => tr.Id,
                (pt, tr) => new { pt.PlaylistId, pt.Position, tr.Id })
            .GroupBy(x => x.PlaylistId)
            .ToDictionary(
                g => g.Key,
                g => g.OrderBy(x => x.Position).Take(4).Select(x => x.Id).ToList());

        return playlists.Select(p => new PlaylistSummary(
            p.Id.Value,
            p.Name,
            p.Description,
            p.IsPublic,
            p.Cover == null ? null : new ImageMetadataDetails(
                p.Cover.ImageId.Value,
                p.Cover.Metadata.Width,
                p.Cover.Metadata.Height,
                p.Cover.Metadata.FileType.Value,
                p.Cover.Metadata.SizeInBytes),
            trackLookup.GetValueOrDefault(p.Id) ?? new List<Guid>()
        )).ToList();
    }
}
