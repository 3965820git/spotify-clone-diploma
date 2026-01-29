using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Accounts.Application.Errors;

public static class UserProfileErrors
{
    public static readonly Error InvalidAvatarImage = new(
        "UserProfile.InvalidAvatarImage",
        "The provided avatar image is invalid.");

    public static readonly Error InvalidBirthDate = new(
        "UserProfile.InvalidBirthDate",
        "The provided birth date is invalid.");

    public static readonly Error InvalidDisplayName = new(
        "UserProfile.InvalidDisplayName",
        "The provided display name is invalid.");

    public static readonly Error InvalidGender = new(
        "UserProfile.InvalidGender",
        "The provided gender is invalid.");

    public static readonly Error AlreadyExists = new(
        "UserProfile.AlreadyExists",
        "The specified user profile is already exists.");
}
