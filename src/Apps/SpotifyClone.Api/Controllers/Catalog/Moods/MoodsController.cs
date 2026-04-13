using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyClone.Api.Mappers;
using SpotifyClone.Catalog.Application.Features.Moods.Queries;
using SpotifyClone.Catalog.Application.Features.Moods.Queries.GetDetails;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Api.Controllers.Catalog.Moods;

[Tags("Catalog Module")]
[Route("api/v1/moods")]
public sealed class MoodsController(IMediator mediator)
    : ApiController(mediator)
{
    [EndpointSummary("Get Mood Details")]
    [EndpointDescription("Returns all the necessary Mood details.")]
    [ProducesResponseType(typeof(MoodDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MoodDetails>> GetMoodDetails(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        Result<MoodDetails> result = await Mediator.Send(
            new GetMoodDetailsQuery(id),
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
}
