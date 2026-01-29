namespace SpotifyClone.Api.Contracts.v1.Accounts.Auth.VerifyPhoneNumber;

public sealed record VerifyPhoneNumberRequest
{
    public required Guid UserId { get; init; }
    public required string PhoneNumber { get; init; }
    public required string Code { get; init; }
}
