using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyClone.Api.Contracts.v1.Streaming.Player.ManipulatePlayback;
using SpotifyClone.Api.Contracts.v1.Streaming.Player.StartPlayback;
using SpotifyClone.Api.Contracts.v1.Streaming.Player.UpdatePosition;
using SpotifyClone.Api.Mappers;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Streaming.Application.Features.Playback.Commands.Pause;
using SpotifyClone.Streaming.Application.Features.Playback.Commands.Resume;
using SpotifyClone.Streaming.Application.Features.Playback.Commands.SeekPosition;
using SpotifyClone.Streaming.Application.Features.Playback.Commands.SkipToNext;
using SpotifyClone.Streaming.Application.Features.Playback.Commands.Start;
using SpotifyClone.Streaming.Application.Features.Playback.Commands.SyncPosition;
using SpotifyClone.Streaming.Application.Features.Playback.Queries;
using SpotifyClone.Streaming.Application.Features.Playback.Queries.GetDetails;

namespace SpotifyClone.Api.Controllers.Streaming;

[Route("api/v1/me/player")]
public sealed class CurrentUserController(IMediator mediator)
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
        Result<PlaybackSessionDetails> result = await Mediator.Send(
            new StartPlaybackCommand(
                request.TrackId,
                request.DeviceId,
                request.ContextType,
                request.ContextExternalId,
                request.PositionMs),
            cancellationToken);
        if (result.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                result,
                HttpContext);

            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        return Ok(new StartPlaybackResponse(result.Value.Id));
    }

    [EndpointSummary("Get Playback Session details")]
    [EndpointDescription("Get current User Playback Session's details.")]
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
}
