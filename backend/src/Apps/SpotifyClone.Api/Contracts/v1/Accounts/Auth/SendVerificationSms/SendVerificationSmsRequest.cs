namespace SpotifyClone.Api.Contracts.v1.Accounts.Auth.SendVerificationSms;

public sealed record SendVerificationSmsRequest
{
    public required Guid UserId { get; init; }
    public required string PhoneNumber { get; init; }
}
