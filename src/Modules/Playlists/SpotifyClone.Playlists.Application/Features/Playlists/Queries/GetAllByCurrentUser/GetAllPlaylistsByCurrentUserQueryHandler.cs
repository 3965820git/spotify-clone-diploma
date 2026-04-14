using SpotifyClone.Playlists.Application.Abstractions.Data;
using SpotifyClone.Playlists.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Queries.GetAllByCurrentUser;

internal sealed class GetAllPlaylistsByCurrentUserQueryHandler(
    IPlaylistReadService playlistReadService,
    ICurrentUser currentUser)
    : IQueryHandler<GetAllPlaylistsByCurrentUserQuery, PlaylistList>
{
    private readonly IPlaylistReadService _playlistReadService = playlistReadService;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<PlaylistList>> Handle(
        GetAllPlaylistsByCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
        {
            return Result.Failure<PlaylistList>(PlaylistErrors.UserNotLoggedIn);
        }

        IEnumerable<PlaylistSummary> playlists =
            await _playlistReadService.GetAllByOwnerAsync(UserId.From(_currentUser.Id), cancellationToken);

        return new PlaylistList(playlists.ToList().AsReadOnly());
    }
}
