using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Catalog.Application.Features.Genres.Queries.GetDetails;

public sealed record GetGenreDetailsQuery(
    Guid GenreId)
    : IQuery<GenreDetails>;
