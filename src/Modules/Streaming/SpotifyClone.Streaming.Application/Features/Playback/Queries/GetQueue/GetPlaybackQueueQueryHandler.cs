using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Abstractions.Data;
using SpotifyClone.Streaming.Application.Errors;

namespace SpotifyClone.Streaming.Application.Features.Playback.Queries.GetQueue;

internal sealed class GetPlaybackQueueQueryHandler(
    IPlaybackSessionReadService playbackSessionReadService,
    ICurrentUser currentUser)
    : IQueryHandler<GetPlaybackQueueQuery, PlaybackQueueDetails>
{
    private readonly IPlaybackSessionReadService _playbackSessionReadService = playbackSessionReadService;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<PlaybackQueueDetails>> Handle(
        GetPlaybackQueueQuery request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
        {
            return Result.Failure<PlaybackQueueDetails>(PlaybackErrors.NotLoggedIn);
        }

        var userId = UserId.From(_currentUser.Id);

        PlaybackSessionDetails? session = await _playbackSessionReadService.GetDetails(
            userId, cancellationToken);
        if (session is null)
        {
            return Result.Failure<PlaybackQueueDetails>(PlaybackErrors.SessionNotFound);
        }

        IEnumerable<Guid> tracksInQueue = await _playbackSessionReadService.GetQueue(
            userId, cancellationToken);

        return new PlaybackQueueDetails(session.TrackId, tracksInQueue);
    }
}
