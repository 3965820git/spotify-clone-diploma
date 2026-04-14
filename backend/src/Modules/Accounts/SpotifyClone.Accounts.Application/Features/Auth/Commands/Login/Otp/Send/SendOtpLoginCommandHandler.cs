using System.Security.Cryptography;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.Otp.Send;

internal sealed class SendOtpLoginCommandHandler(
    IOtpCacheService otpCache,
    ISmsSender smsSender)
    : ICommandHandler<SendOtpLoginCommand, SendOtpLoginCommandResult>
{
    private readonly IOtpCacheService _otpCache = otpCache;
    private readonly ISmsSender _smsSender = smsSender;

    public async Task<Result<SendOtpLoginCommandResult>> Handle(
        SendOtpLoginCommand request,
        CancellationToken cancellationToken)
    {
        string code = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
        var expiresIn = TimeSpan.FromMinutes(5);

        await _otpCache.SaveOtpAsync(
            request.PhoneNumber,
            code,
            expiresIn,
            cancellationToken);

        await _smsSender.SendAsync(
            request.PhoneNumber,
            $"Твій код доступу для SpotifyClone: {code}",
            cancellationToken);

        return new SendOtpLoginCommandResult(expiresIn.TotalSeconds);
    }
}
