using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.LoginWithRefreshToken;

public sealed record LoginWithRefreshTokenCommand(
    string RawToken)
    : IPersistentCommand<LoginWithRefreshTokenCommandResult>;
