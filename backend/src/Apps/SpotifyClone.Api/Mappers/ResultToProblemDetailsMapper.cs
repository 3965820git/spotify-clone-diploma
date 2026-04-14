using System.Net;
using Microsoft.AspNetCore.Mvc;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Api.Mappers;

public static class ResultToProblemDetailsMapper
{
    public static ProblemDetails MapToProblemDetails(
        Shared.BuildingBlocks.Application.Results.IResult result,
        HttpContext httpContext)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Mapper should only be called for failure results.");
        }

        Error primaryError = result.Errors.FirstOrDefault() ?? CommonErrors.Unknown;
        int statusCode = GetStatusCode(primaryError);

        var problemDetails = new ProblemDetails
        {
            Type = $"https://example.com/errors/{primaryError.Code.ToLowerInvariant()}",
            Title = primaryError.Code,
            Status = statusCode,
            Detail = primaryError.Description,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions["errors"] = result.Errors.Select(
            e => new { code = e.Code, description = e.Description }).ToList();

        problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

        return problemDetails;
    }

    private static int GetStatusCode(Error error)
        => error.Code switch
        {
            var c when c.Contains("NotFound") => (int)HttpStatusCode.NotFound,
            var c when c.Contains("Internal") => (int)HttpStatusCode.InternalServerError,
            _ => (int)HttpStatusCode.BadRequest
        };
}
