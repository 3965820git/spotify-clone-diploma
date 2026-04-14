using SpotifyClone.Catalog.Application.Models;

namespace SpotifyClone.Catalog.Application.Features.Moods.Queries;

public sealed record MoodDetails(
    Guid Id,
    string Name,
    ImageMetadataDetails? Cover);
