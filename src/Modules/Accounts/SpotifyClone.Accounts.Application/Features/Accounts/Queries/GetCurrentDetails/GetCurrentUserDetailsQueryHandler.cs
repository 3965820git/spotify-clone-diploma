using SpotifyClone.Accounts.Application.Abstractions.Data;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Queries.GetCurrentDetails;

internal sealed class GetCurrentUserDetailsQueryHandler(
    IUserReadService userReadService,
    ICurrentUser currentUser)
    : IQueryHandler<GetCurrentUserDetailsQuery, CurrentUserDetails>
{
    private readonly IUserReadService _userReadService = userReadService;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<CurrentUserDetails>> Handle(
        GetCurrentUserDetailsQuery request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
        {
            return Result.Failure<CurrentUserDetails>(AuthErrors.NotLoggedIn);
        }

        CurrentUserDetails? user = await _userReadService.GetCurrentDetailsAsync(
            UserId.From(_currentUser.Id),
            cancellationToken);
        if (user is null)
        {
            return Result.Failure<CurrentUserDetails>(UserProfileErrors.NotFound);
        }

        return user;
    }
}
