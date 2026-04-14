using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks.Enums;

namespace SpotifyClone.Catalog.Domain.Services;

public sealed class AlbumTrackDomainService(ITrackRepository tracks)
{
    private readonly ITrackRepository _tracks = tracks;

    public bool TryMarkAlbumAsReadyToPublish(Album album)
    {
        IEnumerable<Track> tracksInAlbum = _tracks.GetByIdsAsync(album.Tracks.Select(t => t.Id)).Result;

        int trackCount = tracksInAlbum.Count(t =>
            t.Status == TrackStatus.ReadyToPublish ||
            t.Status == TrackStatus.Published);

        if (trackCount >= 1 && album.TryMarkAsReadyToPublish())
        {
            return true;
        }

        album.MarkAsDraft();
        return false;
    }

    public void ReevaluateAlbumType(Album album)
    {
        IEnumerable<Track> tracksInAlbum = _tracks.GetByIdsAsync(album.Tracks.Select(t => t.Id)).Result;

        int trackCount = tracksInAlbum.Count(t =>
            t.Status == TrackStatus.ReadyToPublish ||
            t.Status == TrackStatus.Published);

        album.ReevaluateType(trackCount);
    }
}
