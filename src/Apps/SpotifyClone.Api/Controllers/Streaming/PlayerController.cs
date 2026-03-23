using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyClone.Api.Contracts.v1.Streaming.Playback.GetQueue;
using SpotifyClone.Api.Contracts.v1.Streaming.Playback.ManipulatePlayback;
using SpotifyClone.Api.Contracts.v1.Streaming.Playback.StartPlayback;
using SpotifyClone.Api.Contracts.v1.Streaming.Playback.UpdatePosition;
using SpotifyClone.Api.Mappers;
using SpotifyClone.Catalog.Application.Features.Albums.Queries;
using SpotifyClone.Catalog.Application.Features.Albums.Queries.GetDetails;
using SpotifyClone.Catalog.Application.Features.Tracks.Queries;
using SpotifyClone.Catalog.Application.Features.Tracks.Queries.GetAllByIds;
using SpotifyClone.Catalog.Application.Features.Tracks.Queries.GetSummary;
using SpotifyClone.Playlists.Application.Features.Playlists.Queries;
using SpotifyClone.Playlists.Application.Features.Playlists.Queries.GetDetails;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Errors;
using SpotifyClone.Streaming.Application.Features.Playback.Commands.Pause;
using SpotifyClone.Streaming.Application.Features.Playback.Commands.Resume;
using SpotifyClone.Streaming.Application.Features.Playback.Commands.SeekPosition;
using SpotifyClone.Streaming.Application.Features.Playback.Commands.SkipToNext;
using SpotifyClone.Streaming.Application.Features.Playback.Commands.Start;
using SpotifyClone.Streaming.Application.Features.Playback.Commands.SyncPosition;
using SpotifyClone.Streaming.Application.Features.Playback.Queries;
using SpotifyClone.Streaming.Application.Features.Playback.Queries.GetDetails;
using SpotifyClone.Streaming.Application.Features.Playback.Queries.GetQueue;
using SpotifyClone.Streaming.Domain.ValueObjects;

namespace SpotifyClone.Api.Controllers.Streaming;

[Route("api/v1/me/playback")]
public sealed class PlaybackController(IMediator mediator)
    : ApiController(mediator)
{
    [EndpointSummary("Start Playback")]
    [EndpointDescription("With the current User, modifies an existing Playback session or create a new one.")]
    [ProducesResponseType(typeof(StartPlaybackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = UserRoles.Listener)]
    [HttpPost("play")]
    public async Task<ActionResult<StartPlaybackResponse>> Start(
        [FromBody] StartPlaybackRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!PlaybackContext.IsValid(request.ContextType, request.ContextExternalId))
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                Result.Failure<StartPlaybackResponse>(PlaybackErrors.InvalidPlaybackContext),
                HttpContext);
            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        Result<IEnumerable<Guid>> tracksResult = await GetTracksByContextTypeAsync(
            request.ContextType, request.ContextExternalId!.Value, cancellationToken);
        if (tracksResult.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                tracksResult, HttpContext);
            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        Result<PlaybackSessionDetails> playbackResult = await Mediator.Send(
            new StartPlaybackCommand(
                request.DeviceId,
                request.ContextType,
                request.ContextExternalId,
                request.PositionMs,
                tracksResult.Value),
            cancellationToken);
        if (playbackResult.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                playbackResult,
                HttpContext);

            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        return Ok(new StartPlaybackResponse(playbackResult.Value.Id));
    }

    [EndpointSummary("Get Playback Session details")]
    [EndpointDescription("Get current User' Playback Session details.")]
    [ProducesResponseType(typeof(PlaybackSessionDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = UserRoles.Listener)]
    [HttpGet]
    public async Task<ActionResult<PlaybackSessionDetails>> GetDetails(
        CancellationToken cancellationToken = default)
    {
        Result<PlaybackSessionDetails> result = await Mediator.Send(
            new GetPlaybackSessionDetailsQuery(),
            cancellationToken);
        if (result.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                result,
                HttpContext);

            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        return Ok(result.Value);
    }

    [EndpointSummary("Get Playback Queue")]
    [EndpointDescription("Get current User's Playback Queue.")]
    [ProducesResponseType(typeof(GetPlaybackQueueResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = UserRoles.Listener)]
    [HttpGet("queue")]
    public async Task<ActionResult<GetPlaybackQueueResponse>> GetQueue(
        CancellationToken cancellationToken = default)
    {
        Result<PlaybackQueueDetails> queueResult = await Mediator.Send(
            new GetPlaybackQueueQuery(),
            cancellationToken);
        if (queueResult.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                queueResult,
                HttpContext);
            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        Result<TrackSummary> currentTrackResult = await Mediator.Send(
            new GetTrackSummaryQuery(queueResult.Value.CurrentTrackId),
            cancellationToken);
        if (queueResult.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                currentTrackResult,
                HttpContext);
            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        Result<TrackList> tracksInQueueResult = await Mediator.Send(
            new GetAllTracksByIdsQuery(queueResult.Value.TracksInQueue),
            cancellationToken);
        if (tracksInQueueResult.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                tracksInQueueResult,
                HttpContext);
            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        return Ok(new GetPlaybackQueueResponse(
            currentTrackResult.Value,
            tracksInQueueResult.Value.Tracks));
    }

    [EndpointSummary("Resume Playback")]
    [EndpointDescription("With the current User, resumes the playback and updates the Playback session.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = UserRoles.Listener)]
    [HttpPost("resume")]
    public async Task<ActionResult> Resume(
        [FromBody] ManipulatePlaybackRequest request,
        CancellationToken cancellationToken = default)
    {
        Result<ResumePlaybackCommandResult> result = await Mediator.Send(
            new ResumePlaybackCommand(request.DeviceId),
            cancellationToken);
        if (result.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                result,
                HttpContext);

            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        return NoContent();
    }

    [EndpointSummary("Pause Playback")]
    [EndpointDescription("With the current User, pauses the playback and updates the Playback session.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = UserRoles.Listener)]
    [HttpPost("pause")]
    public async Task<ActionResult> Pause(
        [FromBody] ManipulatePlaybackRequest request,
        CancellationToken cancellationToken = default)
    {
        Result<PausePlaybackCommandResult> result = await Mediator.Send(
            new PausePlaybackCommand(request.DeviceId),
            cancellationToken);
        if (result.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                result,
                HttpContext);

            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        return NoContent();
    }

    [EndpointSummary("Sync Playback position")]
    [EndpointDescription("Syncronize current User Playback Session's position (in milliseconds).")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = UserRoles.Listener)]
    [HttpPatch("sync")]
    public async Task<ActionResult> SyncPosition(
        [FromBody] UpdatePlaybackPositionRequest request,
        CancellationToken cancellationToken = default)
    {
        Result<SyncPlaybackPositionCommandResult> result = await Mediator.Send(
            new SyncPlaybackPositionCommand(
                request.DeviceId,
                request.PositionMs),
            cancellationToken);
        if (result.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                result,
                HttpContext);

            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        return NoContent();
    }

    [EndpointSummary("Seek Playback position")]
    [EndpointDescription("Seek current User Playback Session's position (in milliseconds).")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = UserRoles.Listener)]
    [HttpPatch("seek")]
    public async Task<ActionResult> SeekPosition(
        [FromBody] UpdatePlaybackPositionRequest request,
        CancellationToken cancellationToken = default)
    {
        Result<SeekPlaybackPositionCommandResult> result = await Mediator.Send(
            new SeekPlaybackPositionCommand(
                request.DeviceId,
                request.PositionMs),
            cancellationToken);
        if (result.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                result,
                HttpContext);

            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        return NoContent();
    }

    [EndpointSummary("Skip to next Track")]
    [EndpointDescription("Skips the Playback to the next Track in the Queue.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = UserRoles.Listener)]
    [HttpPost("next")]
    public async Task<ActionResult> SkipToNext(
        [FromBody] ManipulatePlaybackRequest request,
        CancellationToken cancellationToken = default)
    {
        Result<SkipToNextTrackCommandResult> result = await Mediator.Send(
            new SkipToNextTrackCommand(request.DeviceId),
            cancellationToken);
        if (result.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                result,
                HttpContext);

            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        return NoContent();
    }

    private async Task<Result<IEnumerable<Guid>>> GetTracksByContextTypeAsync(
        string contextType,
        Guid contextExternalId,
        CancellationToken cancellationToken = default)
    {
        if (contextType == PlaybackContext.AlbumType)
        {
            return await GetTracksByAlbumContextTypeAsync(contextExternalId, cancellationToken);
        }
        else if (contextType == PlaybackContext.PlaylistType ||
                 contextType == PlaybackContext.CollectionType)
        {
            return await GetTracksByPlaylistContextTypeAsync(contextExternalId, cancellationToken);
        }
        else if (contextType == PlaybackContext.SearchType)
        {
            return await GetTracksBySearchContextTypeAsync(contextExternalId, cancellationToken);
        }

        return new List<Guid>();
    }

    private async Task<Result<IEnumerable<Guid>>> GetTracksByAlbumContextTypeAsync(
        Guid contextExternalId,
        CancellationToken cancellationToken = default)
    {
        Result<AlbumDetails> albumResult = await Mediator.Send(
            new GetAlbumDetailsQuery(contextExternalId),
            cancellationToken);
        if (albumResult.IsFailure)
        {
            return Result.Failure<IEnumerable<Guid>>(albumResult.Errors);
        }

        return albumResult.Value.Tracks.Select(t => t.Id).ToList();
    }

    private async Task<Result<IEnumerable<Guid>>> GetTracksByPlaylistContextTypeAsync(
        Guid contextExternalId,
        CancellationToken cancellationToken = default)
    {
        Result<PlaylistDetails> playlistResult = await Mediator.Send(
            new GetPlaylistDetailsQuery(contextExternalId),
            cancellationToken);
        if (playlistResult.IsFailure)
        {
            return Result.Failure<IEnumerable<Guid>>(playlistResult.Errors);
        }

        return playlistResult.Value.Tracks.Select(t => t.Id).ToList();
    }

    private async Task<Result<IEnumerable<Guid>>> GetTracksBySearchContextTypeAsync(
        Guid contextExternalId,
        CancellationToken cancellationToken = default)
    {
        var trackId = TrackId.From(contextExternalId);

        Result<AlbumDetails> albumResult = await Mediator.Send(
            new GetAlbumDetailsQuery(contextExternalId),
            cancellationToken);
        if (albumResult.IsFailure)
        {
            return Result.Failure<IEnumerable<Guid>>(albumResult.Errors);
        }

        var tracks = albumResult.Value.Tracks.Select(t => t.Id).ToList();
        tracks.Remove(trackId.Value);
        tracks.Insert(0, trackId.Value);
        return tracks;
    }
}
