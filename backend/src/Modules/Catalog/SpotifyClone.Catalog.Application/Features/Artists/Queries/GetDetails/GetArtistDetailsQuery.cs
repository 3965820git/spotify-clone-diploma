using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Catalog.Application.Features.Artists.Queries.GetDetails;

public sealed record GetArtistDetailsQuery(
    Guid ArtistId)
    : IQuery<ArtistDetails>;
