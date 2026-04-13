namespace SpotifyClone.Playlists.Application.Features.Playlists.Queries;

public sealed record CollaboratorSummary(
    Guid Id,
    string Name,
    Guid? AvatarImageId);
