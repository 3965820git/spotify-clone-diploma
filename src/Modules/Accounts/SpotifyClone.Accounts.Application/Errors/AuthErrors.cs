using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Accounts.Application.Errors;

public static class AuthErrors
{
    public static readonly Error InvalidIdentifier = new(
        "Auth.InvalidIdentifier",
        "The provided email or username is invalid.");

    public static readonly Error InvalidPassword = new(
        "Auth.InvalidPassword",
        "The provided password is invalid.");

    public static readonly Error SignInNotAllowed = new(
        "Auth.SignInNotAllowed",
        "Sign-in is not allowed for this user.");

    public static readonly Error EmailAlreadyInUse = new(
        "Auth.EmailAlreadyInUse",
        "The specified email is already in use.");

    public static readonly Error RegistrationFailed = new(
        "Auth.RegistrationFailed",
        "Registration of the specified user failed.");

    public static readonly Error NotLoggedIn = new(
        "Auth.NotLoggedIn",
        "User is not authenticated.");

    public static readonly Error EmailAlreadyConfirmed = new(
        "Auth.EmailAlreadyConfirmed",
        "Email is already confirmed.");

    public static readonly Error PhoneNumberAlreadyConfirmed = new(
        "Auth.PhoneNumberAlreadyConfirmed",
        "Phone number is already confirmed.");

    public static readonly Error InvalidEmailConfirmationToken = Identity(
        "InvalidEmailConfirmationToken",
        "Email confirmation token is invalid.");

    public static readonly Error InvalidPhoneNumberConfirmationToken = Identity(
        "InvalidPhoneNumberConfirmationToken",
        "Phone number confirmation token is invalid.");

    public static readonly Error InvalidPasswordResetToken = Identity(
        "InvalidPasswordResetToken",
        "Password reset token is invalid.");

    public static Error Identity(string codeTitle, string description) => new(
        $"Identity.{codeTitle}",
        description);
}
