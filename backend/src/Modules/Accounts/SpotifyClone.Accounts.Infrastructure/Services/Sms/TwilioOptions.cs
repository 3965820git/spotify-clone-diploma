namespace SpotifyClone.Accounts.Infrastructure.Services.Sms;

public sealed record TwilioOptions
{
    public const string SectionName = "Twilio";

    public required string AccountSid { get; init; }
    public required string AuthToken { get; init; }
    public required string FromPhoneNumber { get; init; }
}
