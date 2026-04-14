using SpotifyClone.Catalog.Application.Models;

namespace SpotifyClone.Catalog.Application.Features.Genres.Queries;

public sealed record GenreDetails(
    Guid Id,
    string Name,
    ImageMetadataDetails? Cover);
