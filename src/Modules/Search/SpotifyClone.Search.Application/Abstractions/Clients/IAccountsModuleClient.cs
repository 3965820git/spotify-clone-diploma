using SpotifyClone.Shared.Kernel.Contracts.Accounts;

namespace SpotifyClone.Search.Application.Abstractions.Clients;

public interface IAccountsModuleClient
{
    Task<IEnumerable<UserSharedDto>> GetAllUsersAsync(
        CancellationToken cancellationToken = default);
}
