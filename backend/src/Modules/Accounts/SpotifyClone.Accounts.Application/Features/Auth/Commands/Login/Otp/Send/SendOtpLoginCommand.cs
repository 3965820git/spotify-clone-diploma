using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.Otp.Send;

public sealed record SendOtpLoginCommand(
    string PhoneNumber)
    : ICommand<SendOtpLoginCommandResult>;
