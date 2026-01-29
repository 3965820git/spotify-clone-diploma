using FluentValidation.Results;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Behaviors.Helpers;

public static class ValidationFailureExtensions
{
    public static Error[] ToErrors(this IEnumerable<ValidationFailure> failures)
        => failures.Where(f => f != null).Select(ToError).ToArray();

    public static Error ToError(this ValidationFailure failure)
        => new Error($"Validation.{failure.PropertyName}", failure.ErrorMessage);
}
