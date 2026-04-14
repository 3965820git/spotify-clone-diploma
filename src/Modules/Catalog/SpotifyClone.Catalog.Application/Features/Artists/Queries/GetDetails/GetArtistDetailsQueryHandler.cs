using SpotifyClone.Catalog.Application.Abstractions.Data;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.Enums;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Features.Artists.Queries.GetDetails;

internal sealed class GetArtistDetailsQueryHandler(
    IArtistReadService artistReadService,
    ICurrentUser currentUser)
    : IQueryHandler<GetArtistDetailsQuery, ArtistDetails>
{
    private readonly IArtistReadService _artistReadService = artistReadService;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<ArtistDetails>> Handle(
        GetArtistDetailsQuery request,
        CancellationToken cancellationToken)
    {
        ArtistDetails? artist = await _artistReadService.GetDetailsAsync(
            ArtistId.From(request.ArtistId),
            cancellationToken);
        if (artist is null)
        {
            return Result.Failure<ArtistDetails>(ArtistErrors.NotFound);
        }

        if (artist.Status == ArtistStatus.Banned.Value &&
            !_currentUser.IsAuthenticated && artist.OwnerId != _currentUser.Id &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<ArtistDetails>(ArtistErrors.Banned);
        }

        return artist;
    }
}
