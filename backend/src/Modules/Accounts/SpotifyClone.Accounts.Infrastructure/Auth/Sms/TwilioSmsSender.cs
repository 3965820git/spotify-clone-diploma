using Microsoft.Extensions.Options;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace SpotifyClone.Accounts.Infrastructure.Auth.Sms;

internal sealed class TwilioSmsSender : ISmsSender
{
    private readonly TwilioOptions _options;

    public TwilioSmsSender(IOptions<TwilioOptions> options)
    {
        _options = options.Value;
        TwilioClient.Init(_options.AccountSid, _options.AuthToken);
    }

    public async Task SendAsync(string to, string message)
        => await MessageResource.CreateAsync(
            to: new PhoneNumber(to),
            from: new PhoneNumber(_options.FromPhoneNumber),
            body: message
        );
}
