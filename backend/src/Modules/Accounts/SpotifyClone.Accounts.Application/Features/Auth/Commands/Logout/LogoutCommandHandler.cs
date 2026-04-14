using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

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
            UserId.From(_currentUser.Id),
            replacedByTokenHash: null,
            cancellationToken);

        if (revokeResult.IsFailure)
        {
            return revokeResult;
        }

        return Result.Success();
    }
}
