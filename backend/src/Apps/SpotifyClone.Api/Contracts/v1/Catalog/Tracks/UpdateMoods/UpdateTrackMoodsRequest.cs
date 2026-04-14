namespace SpotifyClone.Api.Contracts.v1.Catalog.Tracks.UpdateMoods;

public sealed record UpdateTrackMoodsRequest
{
    public required IEnumerable<Guid> MoodIds { get; init; }
}
