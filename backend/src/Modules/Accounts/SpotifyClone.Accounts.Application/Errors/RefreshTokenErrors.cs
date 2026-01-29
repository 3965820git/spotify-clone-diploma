using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Accounts.Application.Errors;

public static class RefreshTokenErrors
{
    public static readonly Error InvalidToken = new(
        "RefreshToken.InvalidToken",
        "The specified refresh token is invalid.");

    public static readonly Error NotFound = CommonErrors.NotFound(
        "RefreshToken",
        "Refresh token");

    public static readonly Error Expired = new(
        "RefreshToken.Expired",
        "The specified refresh token is expired.");
}
