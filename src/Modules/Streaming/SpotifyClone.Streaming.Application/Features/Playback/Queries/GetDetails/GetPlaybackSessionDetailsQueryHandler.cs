using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions.Data;
using SpotifyClone.Streaming.Application.Errors;

namespace SpotifyClone.Streaming.Application.Features.Playback.Queries.GetDetails;

internal sealed class GetPlaybackSessionDetailsQueryHandler(
    IPlaybackSessionReadService playbackSessionReadService,
    ICurrentUser currentUser)
    : IQueryHandler<GetPlaybackSessionDetailsQuery, PlaybackSessionDetails>
{
    private readonly IPlaybackSessionReadService _playbackSessionReadService = playbackSessionReadService;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<PlaybackSessionDetails>> Handle(
        GetPlaybackSessionDetailsQuery request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
        {
            return Result.Failure<PlaybackSessionDetails>(PlaybackErrors.NotLoggedIn);
        }

        PlaybackSessionDetails? playbackSession = await _playbackSessionReadService.GetDetails(
            UserId.From(_currentUser.Id), cancellationToken);

        return playbackSession ?? Result.Failure<PlaybackSessionDetails>(PlaybackErrors.SessionNotFound);
    }
}
