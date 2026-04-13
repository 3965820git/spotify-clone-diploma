using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.VerifyEmail;

public sealed record VerifyEmailCommand(
    Guid UserId,
    string Code)
    : ICommand;
