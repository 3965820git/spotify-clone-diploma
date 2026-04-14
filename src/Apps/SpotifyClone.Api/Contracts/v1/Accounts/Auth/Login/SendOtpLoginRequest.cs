namespace SpotifyClone.Api.Contracts.v1.Accounts.Auth.Login;

public sealed record SendOtpLoginRequest
{
    public required string PhoneNumber { get; init; }
}
