using SpotifyClone.Playlists.Application.Models;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Queries;

public sealed record PlaylistDetails(
    Guid Id,
    string Name,
    string? Description,
    Guid OwnerId,
    bool IsPublic,
    ImageMetadataDetails? CustomCoverImageId,
    IEnumerable<Guid> GeneratedCoverImageIds,
    IEnumerable<CollaboratorSummary> Collaborators,
    IEnumerable<PlaylistTrackSummary> Tracks);
