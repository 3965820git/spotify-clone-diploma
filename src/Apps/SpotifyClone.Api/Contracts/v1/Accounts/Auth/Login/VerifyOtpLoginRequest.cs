namespace SpotifyClone.Api.Contracts.v1.Accounts.Auth.Login;

public sealed record VerifyOtpLoginRequest
{
    public required string PhoneNumber { get; init; }
    public required string Code { get; init; }
}
