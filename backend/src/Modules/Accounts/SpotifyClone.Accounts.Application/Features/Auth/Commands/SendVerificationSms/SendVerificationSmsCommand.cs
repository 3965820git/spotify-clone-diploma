using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.SendVerificationSms;

public sealed record SendVerificationSmsCommand(
    Guid UserId,
    string PhoneNumber)
    : ICommand;
