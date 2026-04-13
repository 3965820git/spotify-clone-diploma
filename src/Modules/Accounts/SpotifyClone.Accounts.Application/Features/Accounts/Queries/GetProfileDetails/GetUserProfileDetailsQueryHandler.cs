using SpotifyClone.Accounts.Application.Abstractions.Data;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Queries.GetProfileDetails;

internal sealed class GetUserProfileDetailsQueryHandler(
    IUserReadService userReadService)
    : IQueryHandler<GetUserProfileDetailsQuery, UserProfileDetails>
{
    private readonly IUserReadService _userReadService = userReadService;

    public async Task<Result<UserProfileDetails>> Handle(
        GetUserProfileDetailsQuery request,
        CancellationToken cancellationToken)
    {
        UserProfileDetails? user = await _userReadService.GetProfileDetailsAsync(
            UserId.From(request.UserId),
            cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserProfileDetails>(UserProfileErrors.NotFound);
        }

        return user;
    }
}
