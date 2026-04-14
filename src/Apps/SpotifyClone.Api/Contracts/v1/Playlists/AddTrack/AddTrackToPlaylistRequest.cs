namespace SpotifyClone.Api.Contracts.v1.Playlists.AddTrack;

public sealed record AddTrackToPlaylistRequest
{
    public required Guid TrackId { get; init; }
}
