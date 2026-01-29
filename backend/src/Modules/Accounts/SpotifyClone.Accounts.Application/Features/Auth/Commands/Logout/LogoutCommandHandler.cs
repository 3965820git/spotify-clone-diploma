using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Logout;

internal sealed class LogoutCommandHandler(
    IAccountsUnitOfWork unitOfWork,
    ICurrentUser currentUser)
    : ICommandHandler<LogoutCommand>
{
    private readonly IAccountsUnitOfWork _unit = unitOfWork;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result> Handle(
        LogoutCommand request,
        CancellationToken cancellationToken)
    {
        Result revokeResult = await _unit.RefreshTokens.RevokeAllAsync(
            _currentUser.UserId,
            replacedByTokenHash: null,
            cancellationToken);

        if (revokeResult.IsFailure)
        {
            return revokeResult;
        }

        return Result.Success();
    }
}
