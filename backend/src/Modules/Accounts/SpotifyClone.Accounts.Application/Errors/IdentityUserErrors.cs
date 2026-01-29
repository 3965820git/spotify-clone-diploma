using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Accounts.Application.Errors;

public static class IdentityUserErrors
{
    public static readonly Error NotFound = CommonErrors.NotFound(
        "User", "user");
}
