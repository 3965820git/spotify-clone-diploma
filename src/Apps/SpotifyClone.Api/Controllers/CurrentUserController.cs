using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyClone.Api.Contracts.v1.CurrentUser.StartPlayback;
using SpotifyClone.Api.Mappers;
using SpotifyClone.Playlists.Application.Features.Playlists.Queries;
using SpotifyClone.Playlists.Application.Features.Playlists.Queries.GetAllByCurrentUser;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Streaming.Application.Features.Playback.Commands.StartPlayback;

namespace SpotifyClone.Api.Controllers;

[Route("api/v1/me")]
public sealed class CurrentUserController(IMediator mediator)
    : ApiController(mediator)
{
    [EndpointSummary("Get current User Playlists")]
    [EndpointDescription("Returns the current User's playlists.")]
    [ProducesResponseType(typeof(PlaylistList), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = UserRoles.Listener)]
    [HttpGet("playlists")]
    public async Task<ActionResult<PlaylistList>> GetPlaylists(
        CancellationToken cancellationToken = default)
    {
        Result<PlaylistList> result = await Mediator.Send(
            new GetAllPlaylistsByCurrentUserQuery(),
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

    [EndpointSummary("Start Playback")]
    [EndpointDescription("Starts a playback with the current User.")]
    [ProducesResponseType(typeof(StartPlaybackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = UserRoles.Listener)]
    [HttpPut("play")]
    public async Task<ActionResult<StartPlaybackResponse>> StartPlayback(
        StartPlaybackRequest request,
        CancellationToken cancellationToken = default)
    {
        Result<StartPlaybackCommandResult> result = await Mediator.Send(
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

        return Ok(new StartPlaybackResponse(result.Value.PlaybackSessionId));
    }
}
