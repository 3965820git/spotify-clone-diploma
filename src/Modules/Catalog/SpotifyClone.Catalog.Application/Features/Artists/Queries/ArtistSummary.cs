using SpotifyClone.Catalog.Application.Models;

namespace SpotifyClone.Catalog.Application.Features.Artists.Queries;

public sealed record ArtistSummary(
    Guid Id,
    string Name,
    string Status,
    Guid? OwnerId,
    ImageMetadataDetails? Avatar);
