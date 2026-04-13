using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpotifyClone.Api.Contracts.v1.Catalog.Tracks.Create;
using SpotifyClone.Api.Contracts.v1.Streaming.Media.GetAudioAsset;
using SpotifyClone.Api.Contracts.v1.Streaming.Media.GetImageAsset;
using SpotifyClone.Api.Contracts.v1.Streaming.Media.UploadAudioAsset;
using SpotifyClone.Api.Contracts.v1.Streaming.Media.UploadImageAsset;
using SpotifyClone.Api.Mappers;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Streaming.Application.Features.Media.Commands.UploadAudioAsset;
using SpotifyClone.Streaming.Application.Features.Media.Commands.UploadImageAsset;
using SpotifyClone.Streaming.Application.Features.Media.Queries;
using SpotifyClone.Streaming.Application.Features.Media.Queries.GetAudioAsset;
using SpotifyClone.Streaming.Application.Features.Media.Queries.GetImageAsset;

namespace SpotifyClone.Api.Controllers.Streaming;

[Tags("Streaming Module")]
[Route("api/v1/media")]
public sealed class MediaController(IMediator mediator)
    : ApiController(mediator)
{
    [EndpointSummary("Upload Audio")]
    [EndpointDescription("Starts uploading an audio in the background. " +
        "Note: you will need a track without an audio file linked to it before calling this endpoint.")]
    [ProducesResponseType(typeof(CreateTrackResponse), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost("audio")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<UploadAudioAssetResponse>> UploadAudioAsset(
        [FromForm] UploadAudioAssetRequest request,
        CancellationToken cancellationToken = default)
    {
        using Stream stream = request.File.OpenReadStream();

        Result<UploadAudioAssetCommandResult> result = await Mediator.Send(
            new UploadAudioAssetCommand(
                request.File.FileName,
                stream,
                request.TrackId),
            cancellationToken);
        if (result.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                result,
                HttpContext);

            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        UploadAudioAssetCommandResult resultData = result.Value;

        return Accepted(new UploadAudioAssetResponse(
            resultData.AudioId));
    }

    [EndpointSummary("Get Audio Asset details")]
    [EndpointDescription("Returns all the necessary Audio Asset details.")]
    [ProducesResponseType(typeof(CreateTrackResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("audio")]
    public async Task<ActionResult<GetAudioAssetResponse>> GetAudioAsset(
        [FromQuery] GetAudioAssetRequest request,
        CancellationToken cancellationToken = default)
    {
        Result<AudioAssetDetails> result = await Mediator.Send(
            new GetAudioAssetQuery(
                request.AudioId),
            cancellationToken);
        if (result.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                result,
                HttpContext);
            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        AudioAssetDetails resultData = result.Value;

        return Ok(new GetAudioAssetResponse(
            resultData.AudioId,
            resultData.HlsUrl,
            resultData.DashUrl));
    }

    [EndpointSummary("Upload Image")]
    [EndpointDescription("Starts uploading an image in the background.")]
    [ProducesResponseType(typeof(CreateTrackResponse), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost("images")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<UploadImageAssetResponse>> UploadImageAsset(
        [FromForm] UploadImageAssetRequest request,
        CancellationToken cancellationToken = default)
    {
        using Stream stream = request.File.OpenReadStream();

        Result<UploadImageAssetCommandResult> result = await Mediator.Send(
            new UploadImageAssetCommand(
                request.File.FileName,
                stream),
            cancellationToken);
        if (result.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                result,
                HttpContext);

            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        UploadImageAssetCommandResult resultData = result.Value;

        return Accepted(new UploadImageAssetResponse(
            resultData.ImageId));
    }

    [EndpointSummary("Get Image Asset details")]
    [EndpointDescription("Returns all the necessary Image Asset details.")]
    [ProducesResponseType(typeof(CreateTrackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("images")]
    public async Task<ActionResult<GetImageAssetResponse>> GetImageAsset(
        [FromQuery] GetImageAssetRequest request,
        CancellationToken cancellationToken = default)
    {
        Result<GetImageAssetQueryResult> result = await Mediator.Send(
            new GetImageAssetQuery(
                request.ImageId),
            cancellationToken);
        if (result.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                result,
                HttpContext);
            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        GetImageAssetQueryResult resultData = result.Value;

        return Ok(new GetImageAssetResponse(
            resultData.ImageId,
            resultData.WebpUrl));
    }
}
