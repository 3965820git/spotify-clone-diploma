using MediatR;
using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Repositories;
using SpotifyClone.Accounts.Domain.Aggregates.Users;
using SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Database;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence;

namespace SpotifyClone.Accounts.Infrastructure.Persistence;

internal sealed class AccountsEfCoreUnitOfWork(
    AccountsAppDbContext context,
    IUserProfileRepository userProfiles,
    IRefreshTokenRepository refreshTokens,
    IPublisher publisher)
    : EfCoreUnitOfWorkBase<AccountsAppDbContext>(context, publisher),
    IAccountsUnitOfWork
{
    public IUserProfileRepository UserProfiles => userProfiles;
    public IRefreshTokenRepository RefreshTokens => refreshTokens;
}
