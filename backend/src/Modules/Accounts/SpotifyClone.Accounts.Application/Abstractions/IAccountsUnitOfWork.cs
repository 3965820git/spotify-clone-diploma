using SpotifyClone.Accounts.Application.Abstractions.Repositories;
using SpotifyClone.Accounts.Domain.Aggregates.Users;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions;

namespace SpotifyClone.Accounts.Application.Abstractions;

public interface IAccountsUnitOfWork : IUnitOfWork
{
    IUserProfileRepository UserProfiles { get; }
    IRefreshTokenRepository RefreshTokens { get; }
}
