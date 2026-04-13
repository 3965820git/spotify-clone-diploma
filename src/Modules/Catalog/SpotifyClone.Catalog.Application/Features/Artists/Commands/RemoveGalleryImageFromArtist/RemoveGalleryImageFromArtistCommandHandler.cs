using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.RemoveGalleryImageFromArtist;

internal sealed class RemoveGalleryImageFromArtistCommandHandler(
    ICatalogUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<RemoveGalleryImageFromArtistCommand, RemoveGalleryImageFromArtistCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<RemoveGalleryImageFromArtistCommandResult>> Handle(
        RemoveGalleryImageFromArtistCommand request,
        CancellationToken cancellationToken)
    {
        Artist? artist = await _unit.Artists.GetByIdAsync(
            ArtistId.From(request.ArtistId),
            cancellationToken);
        if (artist is null)
        {
            return Result.Failure<RemoveGalleryImageFromArtistCommandResult>(AlbumErrors.NotFound);
        }

        if ((!_currentUser.IsAuthenticated || _currentUser.Id != artist.OwnerId?.Value) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<RemoveGalleryImageFromArtistCommandResult>(ArtistErrors.NotOwned);
        }

        artist.RemoveGalleryImage(
            ImageId.From(request.ImageId));

        return new RemoveGalleryImageFromArtistCommandResult();
    }
}
