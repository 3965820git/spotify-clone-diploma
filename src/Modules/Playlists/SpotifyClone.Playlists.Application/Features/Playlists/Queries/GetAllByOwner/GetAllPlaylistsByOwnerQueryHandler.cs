using SpotifyClone.Playlists.Application.Abstractions.Data;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Queries.GetAllByOwner;

internal sealed class GetAllPlaylistsByOwnerQueryHandler(
    IPlaylistReadService playlistReadService,
    ICurrentUser currentUser)
    : IQueryHandler<GetAllPlaylistsByOwnerQuery, PlaylistList>
{
    private readonly IPlaylistReadService _playlistReadService = playlistReadService;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<PlaylistList>> Handle(
        GetAllPlaylistsByOwnerQuery request,
        CancellationToken cancellationToken)
    {
        var ownerId = UserId.From(request.OwnerId);

        IEnumerable<PlaylistSummary> playlists =
            _currentUser.IsAuthenticated && ownerId.Value == _currentUser.Id ||
            _currentUser.IsInRole(UserRoles.Admin)
            ? await _playlistReadService.GetAllByOwnerAsync(ownerId, cancellationToken)
            : await _playlistReadService.GetAllPublicByOwnerAsync(ownerId, cancellationToken);

        return new PlaylistList(playlists.ToList().AsReadOnly());
    }
}
