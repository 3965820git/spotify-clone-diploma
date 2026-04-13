using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.Unban;

internal sealed class UnbanArtistCommandHandler(
    ICatalogUnitOfWork unit)
    : ICommandHandler<UnbanArtistCommand, UnbanArtistCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task<Result<UnbanArtistCommandResult>> Handle(
        UnbanArtistCommand request,
        CancellationToken cancellationToken)
    {
        Artist? artist = await _unit.Artists.GetBannedByIdAsync(
            ArtistId.From(request.ArtistId),
            cancellationToken);

        if (artist is null)
        {
            return Result.Failure<UnbanArtistCommandResult>(ArtistErrors.NotFound);
        }

        artist.Unban();

        return new UnbanArtistCommandResult();
    }
}
