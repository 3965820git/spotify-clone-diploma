namespace SpotifyClone.Api.Contracts.v1.Playlists.EditDetails;

public sealed record EditPlaylistDetailsRequest
{
    public required string Name { get; init; }
    public required string? Description { get; init; }
    public required bool IsPublic { get; init; }
}
