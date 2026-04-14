namespace SpotifyClone.Api.Contracts.v1.Accounts.Profile.EditProfileDetails;

public sealed record EditUserProfileDetailsRequest
{
    public required string DisplayName { get; init; }
}
