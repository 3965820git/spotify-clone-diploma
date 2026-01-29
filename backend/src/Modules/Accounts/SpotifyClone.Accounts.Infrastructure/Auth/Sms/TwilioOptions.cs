namespace SpotifyClone.Accounts.Infrastructure.Auth.Sms;

public sealed record TwilioOptions
{
    public const string SectionName = "Twilio";

    public required string AccountSid { get; init; }
    public required string AuthToken { get; init; }
    public required string FromPhoneNumber { get; init; }
}
