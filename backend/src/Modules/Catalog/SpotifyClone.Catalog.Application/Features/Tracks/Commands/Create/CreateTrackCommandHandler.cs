using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Application.Features.Albums.Commands.UnpublishAlbum;
using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Moods.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.Create;

internal sealed class CreateTrackCommandHandler(
    ICatalogUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<CreateTrackCommand, CreateTrackCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<CreateTrackCommandResult>> Handle(
        CreateTrackCommand request,
        CancellationToken cancellationToken)
    {
        var trackId = TrackId.From(Guid.NewGuid());
        var albumId = AlbumId.From(request.AlbumId);

        Album? album = await _unit.Albums.GetByIdAsync(albumId, cancellationToken);
        if (album is null)
        {
            return Result.Failure<CreateTrackCommandResult>(AlbumErrors.NotFound);
        }

        bool artistsExist = await _unit.Artists.Exists(
            request.MainArtists.Select(a => ArtistId.From(a))
                .Concat(request.FeaturedArtists.Select(a => ArtistId.From(a)))
                .ToHashSet(),
            cancellationToken);
        if (!artistsExist)
        {
            return Result.Failure<CreateTrackCommandResult>(ArtistErrors.NotFound);
        }

        IEnumerable<Artist> artists = await _unit.Artists.GetAllByIdsAsync(
            request.MainArtists.Select(a => ArtistId.From(a)),
            cancellationToken);

        if ((!_currentUser.IsAuthenticated || artists.Any(a => a.OwnerId?.Value != _currentUser.Id)) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<CreateTrackCommandResult>(AlbumErrors.NotOwned);
        }

        bool genresExist = await _unit.Genres.Exists(
            request.Genres.Select(g => GenreId.From(g)),
            cancellationToken);
        if (!genresExist)
        {
            return Result.Failure<CreateTrackCommandResult>(GenreErrors.NotFound);
        }

        bool moodsExist = await _unit.Moods.Exists(
            request.Moods.Select(g => MoodId.From(g)),
            cancellationToken);
        if (!moodsExist)
        {
            return Result.Failure<CreateTrackCommandResult>(MoodErrors.NotFound);
        }

        var track = Track.Create(
            trackId,
            request.Title,
            request.ContainsExplicitContent,
            album.Id,
            album.Status.IsPublished,
            request.MainArtists.Select(a => ArtistId.From(a)),
            request.FeaturedArtists.Select(a => ArtistId.From(a)),
            request.Genres.Select(g => GenreId.From(g)),
            request.Moods.Select(m => MoodId.From(m)));

        await _unit.Tracks.AddAsync(track, cancellationToken);

        return new CreateTrackCommandResult(trackId.Value);
    }
}
