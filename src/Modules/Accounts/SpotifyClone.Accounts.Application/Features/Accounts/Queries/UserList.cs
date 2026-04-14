namespace SpotifyClone.Accounts.Application.Features.Accounts.Queries;

public sealed record UserList(
    IEnumerable<UserSummary> Users);
