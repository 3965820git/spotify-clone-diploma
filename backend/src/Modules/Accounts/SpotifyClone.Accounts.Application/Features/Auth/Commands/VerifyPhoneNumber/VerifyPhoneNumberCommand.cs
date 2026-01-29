using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.VerifyPhoneNumber;

public sealed record VerifyPhoneNumberCommand(
    Guid UserId,
    string PhoneNumber,
    string Code)
    : ICommand;
