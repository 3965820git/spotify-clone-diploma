using SpotifyClone.Catalog.Application.Models;

namespace SpotifyClone.Catalog.Application.Features.Artists.Queries;

public sealed record ArtistDetails(
    Guid Id,
    string Name,
    string? Bio,
    Guid? OwnerId,
    string Status,
    ImageMetadataDetails? Avatar,
    ImageMetadataDetails? Banner,
    IEnumerable<ImageMetadataDetails> Gallery);
