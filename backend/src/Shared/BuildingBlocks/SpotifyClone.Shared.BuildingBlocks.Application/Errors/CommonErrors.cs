namespace SpotifyClone.Shared.BuildingBlocks.Application.Errors;

public static class CommonErrors
{
    public static readonly Error Unknown = new Error(
        "Unknown",
        "An unknown error occurred.");

    public static readonly Error InvalidRequest = new Error(
        "InvalidRequest",
        "The request is invalid.");

    public static readonly Error ConcurrencyConflict = new Error(
        "ConcurrencyConflict",
        "A concurrency conflict occurred.");

    public static readonly Error Internal = new Error(
        "Internal",
        "An internal error occurred.");

    public static Error Empty(string codeTitle, string descriptionTitle) => new Error(
        $"{codeTitle}.Empty",
        $"{descriptionTitle} cannot be empty.");

    public static Error TooLong(string codeTitle, string descriptionTitle, short maxLength) => new Error(
        $"{codeTitle}.TooLong",
        $"{descriptionTitle} exceeds the maximum allowed length ({maxLength}).");

    public static Error InvalidFormat(string codeTitle, string descriptionTitle) => new Error(
        $"{codeTitle}.InvalidFormat",
        $"{descriptionTitle} is not in a valid format.");

    public static Error InvalidValue(string codeTitle, string descriptionTitle) => new Error(
        $"{codeTitle}.InvalidValue",
        $"{descriptionTitle} does not have a valid value.");

    public static Error AlreadyExists(string codeTitle, string descriptionTitle) => new Error(
        $"{codeTitle}.AlreadyExists",
        $"{descriptionTitle} already exists.");

    public static Error Null(string codeTitle, string descriptionTitle) => new Error(
        $"{codeTitle}.Null",
        $"{descriptionTitle} cannot be null.");

    public static Error NotFound(string codeTitle, string descriptionTitle) => new Error(
        $"{codeTitle}.NotFound",
        $"{descriptionTitle} was not found.");

    public static Error CannotRemove(string codeTitle, string descriptionTitle, string from) => new Error(
        $"{codeTitle}.CannotRemove",
        $"Cannot remove {descriptionTitle} from {from}");
}
