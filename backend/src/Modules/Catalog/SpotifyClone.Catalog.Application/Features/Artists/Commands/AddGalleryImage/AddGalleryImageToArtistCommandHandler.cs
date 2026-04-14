using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.AddGalleryImage;

internal sealed class AddGalleryImageToArtistCommandHandler(
    ICatalogUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<AddGalleryImageToArtistCommand, AddGalleryImageToArtistCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<AddGalleryImageToArtistCommandResult>> Handle(
        AddGalleryImageToArtistCommand request,
        CancellationToken cancellationToken)
    {
        Artist? artist = await _unit.Artists.GetByIdAsync(
            ArtistId.From(request.ArtistId),
            cancellationToken);
        if (artist is null)
        {
            return Result.Failure<AddGalleryImageToArtistCommandResult>(AlbumErrors.NotFound);
        }

        if ((!_currentUser.IsAuthenticated || _currentUser.Id != artist.OwnerId?.Value) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<AddGalleryImageToArtistCommandResult>(ArtistErrors.NotOwned);
        }

        artist.AddGalleryImage(new ArtistGalleryImage(
            ImageId.From(request.ImageId),
            request.ImageWidth,
            request.ImageHeight,
            ImageFileType.From(request.ImageFileType),
            request.ImageSizeInBytes));

        return new AddGalleryImageToArtistCommandResult();
    }
}
